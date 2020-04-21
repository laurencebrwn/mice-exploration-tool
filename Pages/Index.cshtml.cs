using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft;
using MySql.Data.MySqlClient;
using System.Web;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace miceExplorationTool.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public static string warning;

        public object ViewBag { get; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //FilePath();
        }

        public void OnPost()
        {
            
        }

        //public string errorMessage;

        //display user selected query images
        public void OnPostFilePath()
        {

            //sets filepath as route to dicomImages folder - this is where the user will need to place there images or symlink to
            string filePath = "wwwroot/dicomImages";

            //unit test for filepath name >>result is a console log saying folder does not exist
            //string filePath = "wwwroot/dicomImage";

            //Runs through the folder and assigns a new path then pushes newPath to the database where there is an id match
            if (Directory.Exists(filePath))
            {

                //Creates a new MICE database when the user adds new images or files
                MySqlDatabaseCreation();

                int count = 0; //count for testing purposes

                //calls function to populate mice list with files found at the filePath
                List<Image> mice = FindImages(filePath); //change from findFiles

                foreach (Image im in mice)
                {//goes through them
                    List<string> filepaths = im.GetImages();//gets the "list" of images - this is incase you get any extra files for the same ID
                    foreach (string path in filepaths)
                    {//goes through them

                        count++; //displays a count for testing purposes

                        //removes the wwwroot/ element of the file path and replaces with nothing
                        string myfilePath = path;
                        myfilePath = myfilePath.Replace("wwwroot/", "");

                        //adds 'https://' to start of every item in the list so it can be found in the project root folder
                        string newPath = "https://localhost:5001/" + myfilePath;
                        //Console.WriteLine(newPath);//prints

                        //take last part of file path without extension for search comparison
                        var dirName = Path.GetFileNameWithoutExtension(path);
                        //Console.WriteLine(dirName);//prints file name without extension

   
                        string cmdText = "UPDATE url SET urlString = '" + newPath + " ' WHERE image = '" + dirName + "';";

                        //string cmdText = "UPDATE url SET urlString = '" + newPath + " ' WHERE id = (SELECT image FROM mice WHERE mice.id = '" + dirName + "');";

                        MySqlConnection(cmdText);

                        //string cmdText = "UPDATE url SET urlString = '" + newPath + " ' WHERE id = '" + dirName + "';";
                        //MySqlConnection(cmdText);

                    }

                    //Console.WriteLine("\nNew Images added to database: {0}", count); //displays count for testing purposes
                }
            }
            else
            {
                //sends error to html 
                //errorMessage = "error";
                Console.WriteLine("Folder does not exist. Please check the file path");

            };
            
        }


        public List<string> FindFiles(string directory)//finds all the filepaths in a directory recursively
        {
            List<string> filepaths = new List<string>();

            string[] Directories = Directory.GetDirectories(@directory);//Allows for copy paste filepaths using Override. - this holds all the sub- directories in a directory folder
            if (Directories.Length > 0)//Checks if there are any sub folders at this level
            {
                //Console.WriteLine(Directories[0]);
                foreach (string Dir in Directories)//if there is, iterate through the list
                {
                    filepaths.AddRange(FindFiles(Dir));//call function recursively - adds the returned list elements to the filepaths list
                }
                filepaths.AddRange(Directory.GetFiles(@directory));//adds all the files in this folder to the list
            }
            else//if this is a bottom of the tree sub directory
            {
                filepaths.AddRange(Directory.GetFiles(@directory));//adds all the files in this folder to the list
            }
            return filepaths;//returns list of filepaths.
        }

        ///////////////////////////////// Williams Implimentation /////////////////////////////////////////////////////////////


        public List<Image> FindImages(string DirectoryPath)
        //public List<Image> FindFiles(string DirectoryPath) //current working
        {

            //string[] Dir = Directory.GetFiles(DirectoryPath);//Allows for copy paste filepaths using Override.

            List<string> Dir = FindFiles(DirectoryPath);
            List<string> ImagePaths = new List<string>();//creates a list of filepaths
            List<string> TagPaths = new List<string>();//creates a list of filepaths

            foreach (string file in Dir)
            {//goes through directory finding all files
                string ext = Path.GetExtension(file);

                //NOTE: .TIF and .BMP files are regsiterd in the app and databse but the Cornertone viewer does not recognise these as valid DICOM P10 files. See https://github.com/cornerstonejs/dicomParser/issues/112
                if (ext == ".dcm") //If extension is a dcm file then put in ImagePaths list
                {//sorts dependent upont file type
                    ImagePaths.Add(file);
                }
                else
                {
                    TagPaths.Add(file); //this list stores the json and txt file paths
                }

            }
            List<Image> Images = SortImage(ImagePaths, TagPaths);//Sorts images of the same mouse into record.
            return Images;//returns a list of the Image object
        }



        public List<Image> SortImage(List<string> Files, List<string> Tags)//sorts all the image filepaths and the tag filepaths into an Image object containing a list of the tag objects and the image filepahts
        {
            List<string> Filepaths = Files;//takes all the filepaths and stores them
            List<Image> Images = new List<Image>();//list of the Image objects
            List<Tags> TagData = new List<Tags>();
            List<List<Tags>> SortedTags = new List<List<Tags>>();
            TagData = getTags(Tags);//Retrieves all the tag data and stores in the Tag data object, and adds to the list
            SortedTags = SortLists(TagData);//Groups together all related Tag objects
            foreach (string File in Filepaths)//iterates throught the image filepaths, and matches to the tags, and stores in an Image object
            {
                List<string> files = new List<string>();
                string id = GetID(Filepaths[0]);//gets the ID from the image
                files.Add(File);//adds the 
                List<Tags> t = FindList(id, SortedTags);
                Image Mouse = new Image(id);
                Mouse.SetImage(files);
                Mouse.SetTags(t);
                Images.Add(Mouse);

            }
            return Images;
        }


        private List<Tags> FindList(string ID, List<List<Tags>> sortedTags)//finds the list that matches the ID given
        {
            List<Tags> match = new List<Tags>();
            foreach (List<Tags> TagList in sortedTags)//iterates through the list of lists
            {
                bool found = false;
                foreach (Tags t in TagList)//searches through each Tag group in the list.
                {
                    if (t.download_file_path != null)//Not all of the JSON objects have this element.
                    {
                        string TagID = GetID(t.download_file_path);//gets the ID and tries to match
                        if (TagID == ID)//if it's a match
                        {
                            found = true;//set to true
                            break;//stop iteration
                        }
                    }
                }
                if (found == true)//end of loop, if true, assign this list to the match list
                {
                    match = TagList;
                    break;
                }
            }
            return match;//return list
        }


        public static string GetID(string filepath)//this method fetches the id from the filename.
        {

            if (filepath != null) //Not all json or text files will have file paths (raw data jsons don't for instance)
            {
                Regex regex = new Regex(@"^\d$");//regex for multiple 0-9 digits
                int start = 0;
                string FileName = Path.GetFileNameWithoutExtension(filepath);//retrieves the filenames with out the .extension
                for (int i = FileName.Length - 1; i >= 0; i--)//iterates through the filename until it finds where the id ends
                {
                    if (!regex.IsMatch(FileName[i].ToString()))
                    {
                        start = i + 1;//sets the start point of the numbers
                        break;
                    }
                }
                string ID = FileName.Substring(start);//gets the substring of the id
                return ID;//returns the id;
            }

            return null;
        }


        public List<Tags> getTags(List<string> files)
        {//gets a list of files, and returns a list of the JObjects
            List<Tags> TagList = new List<Tags>();

            foreach (string file in files)
            {//iterates through list

                List<Tags> TagIn = ReadInFile(file);
                if (TagIn != null) // Ignore if a returned file is null. Should only be .txt or .json
                {
                    TagList.AddRange(TagIn);
                }
            }
            return TagList;
        }


        //Reads file in from user input and checks if it is a .txt or .json. If not returns a serror to the user. 
        public List<Tags> ReadInFile(string Filepath)//Finds all JSON objects in the path and stores the relevant tags in the list of object type Tags
        {

            string fileExtension = Path.GetExtension(Filepath);

            if (fileExtension == ".txt" || fileExtension == ".json")
            {

                string JSON = System.IO.File.ReadAllText(@Filepath);
                //Console.WriteLine(JSON);
                JObject TagResults = JObject.Parse(JSON);
                //Console.WriteLine(TagResults.ToString());
                IList<JToken> TagTokens = TagResults["response"]["docs"].Children().ToList();
                List<Tags> TagLists = new List<Tags>();

                foreach (JToken Token in TagTokens)
                {
                    Tags t = Token.ToObject<Tags>();
                    TagLists.Add(t);
                    //t.writeout();

                    t.populateMySqlDatabase(); //populates the database with the respective tags

                    //Console.WriteLine(); //a line for spacing for each sample during testing

                }

                return TagLists;

            }

            return null; // A file should only be a .txt or .json. Anything else will return null. 

        }


        public List<List<Tags>> SortLists(List<Tags> Input) //sorts the lists of tags into groups based upon the shared tag of biological_sample_ID
        {
            List<Tags> TagList = Input;
            List<List<Tags>> SortedList = new List<List<Tags>>();
            bool sorted = false;
            while (sorted == false)//loops until correct
            {
                List<Tags> t = new List<Tags>();//tag for group being sorted
                List<int> Indexes = new List<int>();//list of the indexes to be removed
                string BioId = TagList[0].biological_sample_id;
                t.Add(TagList[0]);//adds the tag list at index 0
                Indexes.Add(0);//adds the indext to it
                for (int i = 1; i < TagList.Count; i++)//iterates through the list starting at index 1
                {
                    if (TagList[i].biological_sample_id == BioId)//if a mathc
                    {
                        t.Add(TagList[i]);//add to list
                        Indexes.Add(i);//add index
                    }
                }
                SortedList.Add(t);//add group list to the sorted list group
                for (int i = Indexes.Count - 1; i >= 0; i--)//removes indexes in reverse order. This handles the list auto-resizing.
                {
                    TagList.RemoveAt(Indexes[i]);
                }

                if (TagList.Count == 0)//If the taglist is empty it has been sorted
                {
                    sorted = true;//finish clause
                }

            }
            return SortedList;
        }


        public class Image//Record of images
        {
            private string ID { get; set; }

            private string biological_sample_id { get; set; }
            private List<string> ImageFilepaths = new List<string>();//stores the groups of images
            private List<Tags> TagData = new List<Tags>();

            public Image(string ID)
            {//creates a new object
                this.ID = ID;
            }

            public void SetImage(List<string> Filepath)
            {//sets the list of images to the image
                ImageFilepaths = Filepath;
            }

            public void SetTags(List<Tags> TagList)
            {
                this.TagData = TagList;
            }

            public List<string> GetImages()
            {//returns the list of images
                return this.ImageFilepaths;
            }

            public List<Tags> GetTagData()
            {
                return this.TagData;
            }

        }


        public class Tags
        {

            public string id { get; set; }//These are the data set that we are presenting on the site or for data purposes.
            public string date_of_birth { get; set; }
            public string sex { get; set; }
            public string age_in_weeks { get; set; }
            public string weight { get; set; }
            public string biological_sample_id { get; set; }
            public string gene_symbol { get; set; }
            public string gene_accession_id { get; set; }
            public string zygosity { get; set; }
            public string parameter_name { get; set; }
            public string phenotyping_center { get; set; }
            public string observation_type { get; set; }


            //public string biological_sample_group { get; set; }
            //public string category { get; set; }
            public string download_file_path { get; set; }


            [JsonExtensionData]
            private IDictionary<string, JToken> _extraStuff;


            public void writeout()//Testing use that prints out the data if necessary
            {
                Console.WriteLine("id: {0}", id);
                Console.WriteLine("date_of_birth: {0}", date_of_birth);
                Console.WriteLine("sex: {0}", sex);
                Console.WriteLine("age_in_weeks: {0}", age_in_weeks);
                Console.WriteLine("weight: {0}", weight);
                Console.WriteLine("Biological_sample_id: {0}", biological_sample_id);
                Console.WriteLine("gene_symbol: {0}", gene_symbol);
                Console.WriteLine("gene_accession_id: {0}", gene_accession_id);
                Console.WriteLine("zygosity: {0}", zygosity);
                Console.WriteLine("parameter_name: {0}", parameter_name);
                Console.WriteLine("phenotyping_center: {0}", phenotyping_center);
                Console.WriteLine("observation_type: {0}", observation_type);

                //Console.WriteLine("biological_sample_group: {0}", biological_sample_group);
                //Console.WriteLine("catagory: {0}", category);
                //Console.WriteLine(download_file_path);
            }


            public void populateMySqlDatabase()//creates a new record in the SQL database
            {

                string image = GetID(download_file_path); //name of image in users folder


                string populateDatabase = "insert IGNORE into `mice` " +
                    "(`id`,`image`,`date_of_birth`,`sex`,`age_in_weeks`,`weight`,`Biological_sample_id`,`gene_symbol`,`gene_accession_id`,`zygosity`,`parameter_name`,`phenotyping_center`,`observation_type`) values" +
                    "('" + id + "', " +
                    "'" + image + "', " +
                    "'" + date_of_birth + "', " +
                    "'" + sex + "', " +
                    "'" + age_in_weeks + "', " +
                    "'" + weight + "', " +
                    "'" + biological_sample_id + "', " +
                    "'" + gene_symbol + "', " +
                    "'" + gene_accession_id + "', " +
                    "'" + zygosity + "', " +
                    "'" + parameter_name + "', " +
                    "'" + phenotyping_center + "', " +
                    "'" + observation_type + "');" +
                    "INSERT INTO `url` (`id`, `image`)" + 
                    "SELECT '" + id + "', '" + image + "' " +
                    "WHERE NOT EXISTS (SELECT * FROM `url` " + 
                    "WHERE `id` = '" + id + "' LIMIT 1);";


                CreateMySqlDatabase(populateDatabase);

            }

        }

        ////////////////////////////////// Williams Implimentation /////////////////////////////////////////////////////////////


        //Creates the database in prepartion for population. Only has to do this each time a database is updated.
        public void MySqlDatabaseCreation()
        {

            //Creates the MICE table with the required field headers (hard coded)
            string createDbTableMice = "DROP DATABASE IF EXISTS MICE;" +
                "CREATE DATABASE MICE;" +
                "USE MICE;" +
                "CREATE TABLE IF NOT EXISTS `mice` " +
                "(`id` varchar(50) NOT NULL," +
                "`image` varchar(50) NOT NULL," +
                "`date_of_birth` varchar(50) NOT NULL," +
                "`sex` varchar(50) NOT NULL," +
                "`age_in_weeks` varchar(50) NOT NULL," +
                "`weight` varchar(100) NOT NULL," +
                "`biological_sample_id` varchar(50) NOT NULL," +
                "`gene_symbol` varchar(50) NOT NULL," +
                "`gene_accession_id` varchar(50) NOT NULL," +
                "`zygosity` varchar(50) NOT NULL," +
                "`parameter_name` varchar(50) NOT NULL," +
                "`phenotyping_center` varchar(50) NOT NULL," +
                "`observation_type` varchar(50) NOT NULL," +
                "PRIMARY KEY(`id`), KEY `id` (`id`) ) ENGINE = InnoDB DEFAULT CHARSET = latin1;" +
                "CREATE TABLE IF NOT EXISTS `url` " +
                "(`id` varchar(50) NOT NULL," +
                "`image` varchar(50) NULL," +
                "`urlString` varchar(100) NULL," +
                "KEY `id` (`id`)," +
                "CONSTRAINT `orders_ibfk_1` FOREIGN KEY(`id`) REFERENCES `mice` (`id`)) " +
                "ENGINE = InnoDB DEFAULT CHARSET = latin1;";
            CreateMySqlDatabase(createDbTableMice);

        }


        //Creates database for list of header tags
        public static void CreateMySqlDatabase(string connection)
        {

            // Opens a db connection using localhost database connection.Could also have used 127.0.0.1 instead of localhost
            String str = @"server=localhost; userid=root; password=TSEGroup34;"; //change this to your required userid and password
            MySqlConnection conn = null;

            string cmdText = connection;

            try //To open localhost database and present a query
            {
                //Create a object with 'str' connection values passed. This uses the inbuilt library of MySql which is required
                conn = new MySqlConnection(str);
                conn.Open(); //opens the database connection
                //Console.WriteLine("Localhost MySQL Database Created"); //If the database opens it presents this messsge. 

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                cmd.ExecuteNonQuery(); //executes a non query and returns no data

                //Console.WriteLine("\nDatabase Entry Created");

            }
            catch (MySqlException errorMessage) //Prints exception if the connection cannot be opened (wrong password etc)
            {
                Console.WriteLine("You have not connected to the database: \n\n", errorMessage);

            }
            finally //Once the try-ctach block is complete the connection is closed
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            
        }


        //Main functon that connects to the MySql server and returns the reuqired URLs
        public void MySqlConnection(string connection)
        {

            // Opens a db connection using localhost database connection.Could also have used 127.0.0.1 instead of localhost
            String str = @"server=localhost; database=MICE; userid=root; password=TSEGroup34;"; //change this to your required userid and password
            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            string cmdText = connection;

            try //To open localhost database and present a query
            {
                //Create a object with 'str' connection values passed. This uses the inbuilt library of MySql which is required
                conn = new MySqlConnection(str);
                conn.Open(); //opens the database connection
                //Console.WriteLine("Localhost MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to a list that will be passed to client side
                List<string> urlList = new List<string>();

                while (reader.Read())
                {
                    //Console.WriteLine(reader.GetString(0));
                    urlList.Add(reader.GetString(0));

                }

                ViewData["DICOMArrayList"] = urlList;

            }
            catch (MySqlException errorMessage) //Prints exception if the connection cannot be opened (wrong password etc)
            {
                Console.WriteLine(errorMessage);
            }
            finally //Once the try-ctach block is complete the connection is closed
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }


    }

}
