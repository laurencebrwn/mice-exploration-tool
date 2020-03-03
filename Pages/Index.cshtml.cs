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

        }

        public string errorMessage;

        //display user selected query images
        public void OnPostFilePath(string filePath)
        {

            //Checks if filepath exists
            if (Directory.Exists(filePath))
            {

                List<Image> mice = FindFiles(filePath);
                foreach (Image im in mice)
                {//goes through them
                    List<string> filepaths = im.GetImages();//gets the "list" of images - this is incase you get any extra files for the same ID
                    foreach (string path in filepaths)
                    {//goes through them


                        //adds 'file://' to start of every item in the list
                        string newPath = "file://" + path;
                        //Console.WriteLine(path);//prints

                        //take last part of file path without extension
                        var dirName = Path.GetFileNameWithoutExtension(path);
                        Console.WriteLine(dirName);//prints file name without extension

                        //create a insert funtion that uses the comparion and seraches the database and inserts the PATH value

                        //string cmdText = "UPDATE url SET urlString = '" + newPath + " ' WHERE patient_id = '" + dirName + "';";
                        //MySqlConnection(cmdText);

                    }

                }

            }
            else
            {
                //sends error to html 
                errorMessage = "error";
                Console.WriteLine("Folder does not exist");

            };

        }

        public List<Image> FindFiles(string DirectoryPath)
        {

            string[] Dir = Directory.GetFiles(DirectoryPath);//Allows for copy paste filepaths using Override.
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
            List<Image> Images = SortImage(ImagePaths);//Sorts images of the same mouse into record.

            return Images;//returns a list of the Image object

        }

        public List<Image> SortImage(List<string> Files)
        {
            List<string> Filepaths = Files;
            List<Image> Images = new List<Image>();
            foreach (string File in Filepaths)
            {
                List<string> files = new List<string>();
                //string id = GetID(Filepaths[0]);
                files.Add(File);
                Image Mouse = new Image();
                Mouse.AddImage(files);
                Images.Add(Mouse);

            }

            return Images;
        }

        public void GetImagesPaths(string fileURL)
        {//this calls the find files method, and gets and object of images back - I've left it void for you.
            List<Image> mice = FindFiles(fileURL);
            foreach (Image im in mice)
            {//goes through them
                List<string> filepaths = im.GetImages();//gets the "list" of images - this is incase you get any extra files for the same ID
                foreach (string path in filepaths)
                {//goes through them
                    Console.WriteLine(filepaths);//prints
                }

            }
        }


        //Main functon that connects to the ySql server and returns the reuqired URLs
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
    }
}
