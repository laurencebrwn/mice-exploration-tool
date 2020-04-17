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

                Console.WriteLine("\nNew Images added to database:");

                //calls function to populate mice list with files found at the filePath
                List<Image> mice = FindImages(filePath); //change from findFiles

                foreach (Image im in mice)
                {//goes through them
                    List<string> filepaths = im.GetImages();//gets the "list" of images - this is incase you get any extra files for the same ID
                    foreach (string path in filepaths)
                    {//goes through them

                        //removes the wwwroot/ element of the file path
                        string myfilePath = path;
                        myfilePath = myfilePath.Replace("wwwroot/", "");

                        //adds 'https://' to start of every item in the list so it can be found in the project root folder
                        string newPath = "https://localhost:5001/" + myfilePath;
                        Console.WriteLine(newPath);//prints

                        //take last part of file path without extension for search comparison
                        var dirName = Path.GetFileNameWithoutExtension(path);
                        //Console.WriteLine(dirName);//prints file name without extension

                        //create a insert funtion that uses the comparison and searches the database and inserts the newPath value
                        string cmdText = "UPDATE url SET urlString = '" + newPath + " ' WHERE patient_id = '" + dirName + "';";
                        MySqlConnection(cmdText);

                    }

                }
            }
            else
            {
                //sends error to html 
                //errorMessage = "error";
                Console.WriteLine("Folder does not exist. Please check the file path");

            };

        }


        //public List<Image> FindImages(string DirectoryPath)
        ////public List<Image> FindFiles(string DirectoryPath) //current working
        //{

        //    //string[] Dir = Directory.GetFiles(DirectoryPath);//Allows for copy paste filepaths using Override.

        //    List<string> Dir = FindFiles(DirectoryPath);
        //    List<string> ImagePaths = new List<string>();//creates a list of filepaths
        //    List<string> TagPaths = new List<string>();//creates a list of filepaths

        //    foreach (string file in Dir)
        //    {//goes through directory finding all files
        //        string ext = Path.GetExtension(file);

        //        if (ext == ".dcm") //If extension is a dcm file then put in ImagePaths list
        //        {//sorts dependent upont file type
        //            ImagePaths.Add(file);
        //        }
        //        else
        //        {
        //            TagPaths.Add(file);
        //        }

        //    }
        //    List<Image> Images = SortImage(ImagePaths);//Sorts images of the same mouse into record.
        //    return Images;//returns a list of the Image object
        //}


        public List<string> FindFiles(string directory)
        {
            List<string> filepaths = new List<string>();

            string[] Directories = Directory.GetDirectories(@directory);//Allows for copy paste filepaths using Override.
            if (Directories.Length > 0)
            {
                Console.WriteLine(Directories[0]);
                foreach (string Dir in Directories)
                {
                    filepaths.AddRange(FindFiles(Dir));
                }
                filepaths.AddRange(Directory.GetFiles(@directory));
            }
            else
            {
                filepaths.AddRange(Directory.GetFiles(@directory));
            }
            return filepaths;
        }

        //public List<Image> SortImage(List<string> Files)
        //{
        //    List<string> Filepaths = Files;
        //    List<Image> Images = new List<Image>();
        //    foreach (string File in Filepaths)
        //    {
        //        List<string> files = new List<string>();
        //        //string id = GetID(Filepaths[0]);
        //        files.Add(File);
        //        Image Mouse = new Image();
        //        Mouse.AddImage(files);
        //        Images.Add(Mouse);
        //    }
        //    return Images;
        //}




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

                if (ext == ".dcm") //If extension is a dcm file then put in ImagePaths list
                {//sorts dependent upont file type
                    ImagePaths.Add(file);
                }
                else
                {
                    TagPaths.Add(file);
                }

            }
            List<Image> Images = SortImage(ImagePaths, TagPaths);//Sorts images of the same mouse into record.
            return Images;//returns a list of the Image object
        }



        public List<Image> SortImage(List<string> Files, List<string> Tags)
        {
            List<string> Filepaths = Files;
            List<Image> Images = new List<Image>();
            List<Tags> TagData = new List<Tags>();
            List<List<Tags>> SortedTags = new List<List<Tags>>();
            TagData = getTags(Tags);
            SortedTags = SortLists(TagData);
            foreach (string File in Filepaths)
            {
                List<string> files = new List<string>();
                string id = GetID(Filepaths[0]);
                files.Add(File);
                List<Tags> t = FindList(id, SortedTags);
                Image Mouse = new Image(id);
                Mouse.SetImage(files);
                Mouse.SetTags(t);
                Images.Add(Mouse);

            }
            return Images;
        }


        private List<Tags> FindList(string ID, List<List<Tags>> sortedTags)
        {
            List<Tags> match = new List<Tags>();
            foreach (List<Tags> TagList in sortedTags)
            {
                bool found = false;
                foreach (Tags t in TagList)
                {
                    if (t.download_file_path != null)
                    {
                        string TagID = GetID(t.download_file_path);
                        if (TagID == ID)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (found == true)
                {
                    match = TagList;
                    break;
                }
            }
            return match;
        }


        string GetID(string filepath)
        {
            Regex regex = new Regex(@"^\d$");
            int start = 0;
            string FileName = Path.GetFileNameWithoutExtension(filepath);
            for (int i = FileName.Length - 1; i >= 0; i--)
            {
                if (!regex.IsMatch(FileName[i].ToString()))
                {
                    start = i + 1;
                    break;
                }
            }
            string ID = FileName.Substring(start);
            return ID;
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
                    t.writeout();







                    //add here the database connection and creation for each header. Make sure it does each tag. 
                    string cmdText = "UPDATE url SET urlString = '" + newPath + " ' WHERE patient_id = '" + dirName + "';";
                    MySqlConnection(cmdText);






                }

                return TagLists;

            }

            return null; // A file should only be a .txt or .json. Anything else will return null. 

        }


        public List<List<Tags>> SortLists(List<Tags> Input)
        {
            List<Tags> TagList = Input;
            List<List<Tags>> SortedList = new List<List<Tags>>();
            bool sorted = false;
            while (sorted == false)
            {
                List<Tags> t = new List<Tags>();
                List<int> Indexes = new List<int>();
                string BioId = TagList[0].biological_sample_id;
                t.Add(TagList[0]);
                Indexes.Add(0);
                for (int i = 1; i < TagList.Count; i++)
                {
                    if (TagList[i].biological_sample_id == BioId)
                    {
                        t.Add(TagList[i]);
                        Indexes.Add(i);
                    }
                }
                SortedList.Add(t);
                for (int i = Indexes.Count - 1; i >= 0; i--)
                {
                    TagList.RemoveAt(Indexes[i]);
                }

                if (TagList.Count == 0)
                {
                    sorted = true;
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
            public string biological_sample_id { get; set; }
            public string phenotyping_center { get; set; }
            public string date_of_birth { get; set; }
            public string sex { get; set; }
            public string age_in_weeks { get; set; }
            public string weight { get; set; }
            public string biological_sample_group { get; set; }
            public string gene_symbol { get; set; }
            public string zygosity { get; set; }
            public string observation_type { get; set; }
            public string category { get; set; }

            public string download_file_path { get; set; }

            [JsonExtensionData]
            private IDictionary<string, JToken> _extraStuff;



            public void writeout()
            {
                Console.WriteLine(biological_sample_id);
                Console.WriteLine(phenotyping_center);
                Console.WriteLine(date_of_birth);
                Console.WriteLine(sex);
                Console.WriteLine(age_in_weeks);
                Console.WriteLine(weight);
                Console.WriteLine(biological_sample_group);
                Console.WriteLine(gene_symbol);
                Console.WriteLine(zygosity);
                Console.WriteLine(observation_type);
                Console.WriteLine(category);
                Console.WriteLine(download_file_path);
            }
        }

        //public void writeout()
        //{
        //    Console.WriteLine(biological_sample_id);
        //    Console.WriteLine(phenotyping_center);
        //    Console.WriteLine(date_of_birth);
        //    Console.WriteLine(sex);
        //    Console.WriteLine(age_in_weeks);
        //    Console.WriteLine(weight);
        //    Console.WriteLine(biological_sample_group);
        //    Console.WriteLine(gene_symbol);
        //    Console.WriteLine(zygosity);
        //    Console.WriteLine(observation_type);
        //    Console.WriteLine(category);
        //    Console.WriteLine(download_file_path);

        //}



        ////////////////////////////////// Williams Implimentation /////////////////////////////////////////////////////////////






        //public void GetImagesPaths(string fileURL)
        //{//this calls the find files method, and gets and object of images back - I've left it void for you.
        //    List<Image> mice = FindImages(fileURL); //was findFiles
        //    foreach (Image im in mice)
        //    {//goes through them
        //        List<string> filepaths = im.GetImages();//gets the "list" of images - this is incase you get any extra files for the same ID
        //        foreach (string path in filepaths)
        //        {//goes through them
        //            Console.WriteLine(filepaths);//prints
        //        }

        //    }
        //}


        //Main functon that connects to the MySql server and returns the reuqired URLs
        public void MySqlConnection(string connection)
        {

            // Opens a db connection using localhost database connection.Could also have used 127.0.0.1
            String str = @"server=localhost; database=MICE; userid=root; password=TSEGroup34;";
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

    //Import the users file path

    public class Image//Record of images
    {
        private List<string> ImageFilepaths = new List<string>();//stores the groups of images
        private List<string> TagHeaders = new List<string>();
        private List<string> TagInfo = new List<string>();

        public Image()
        {//creates a new object

        }

        public void AddImage(List<string> filepath)
        {//sets the list of images to the image
            ImageFilepaths = filepath;
        }

        public void AddTagInfo(List<string> headers, List<string> body)
        {//sets the info as 2 a
            this.TagHeaders = headers;
            this.TagInfo = body;
        }


        public List<string> GetImages()
        {//returns the list of images
            return this.ImageFilepaths;
        }

        public List<string> GetHeaders()
        {
            return this.TagHeaders;
        }

        public List<string> GetInfo()
        {
            return this.TagInfo;
        }

        internal void SetImage(List<string> files)
        {
            throw new NotImplementedException();
        }
    }

}
