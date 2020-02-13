using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;
using MySql.Data.MySqlClient;

namespace helloWorld.Pages
{
    public class dicomImageToGridModel : PageModel
    {
        private readonly ILogger<dicomImageToGridModel> _logger;

        public dicomImageToGridModel(ILogger<dicomImageToGridModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            // Opens a db connection using localhost database connection.Could also have used 127.0.0.1
            String str = @"server=localhost; database=MICE; userid=root; password=TSEGroup34;";
            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            try //To open localhost database and present a query
            {
                //Create a object with 'str' connection values passed. This uses the inbuilt library of MySql which is required
                conn = new MySqlConnection(str);
                conn.Open(); //opens the database connection
                Console.WriteLine("Localhost MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Assigns a query to cmdText which will be passed to the MySql database
                //Returns URLs of all female mice where the patient_id's match (because data is in 2 tables)

                //string cmdText = @"
                //SELECT urlString FROM url WHERE patient_id IN 
                //(SELECT patient_id FROM mice WHERE patient_sex = 'F');";

                //shows all images in database via URLs
                string cmdText = @"
                SELECT urlString FROM MICE.url;";


                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to an array that will be passed to client side
                int i = 0;
                string[] arrayURL = new string[25];
                while (reader.Read())
                {
                    //Console.WriteLine(reader.GetString(0));
                    arrayURL[i] = reader.GetString(0);
                    i++;
                }

                ViewData["DICOMArrayList"] = arrayURL;

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


        //shows only female samples
        public IActionResult OnPostFemales()
        {

            // Opens a db connection using localhost database connection.Could also have used 127.0.0.1
            String str = @"server=localhost; database=MICE; userid=root; password=TSEGroup34;";
            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            try //To open localhost database and present a query
            {
                //Create a object with 'str' connection values passed. This uses the inbuilt library of MySql which is required
                conn = new MySqlConnection(str);
                conn.Open(); //opens the database connection
                Console.WriteLine("Localhost MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Assigns a query to cmdText which will be passed to the MySql database
                //Returns URLs of all female mice where the patient_id's match (because data is in 2 tables)
                string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE patient_sex = 'F');";

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to an array that will be passed to client side
                int i = 0;
                string[] arrayURL = new string[25];
                while (reader.Read())
                {
                    //Console.WriteLine(reader.GetString(0));
                    arrayURL[i] = reader.GetString(0);
                    i++;
                }

                ViewData["DICOMArrayList"] = arrayURL;

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

            return Page();

        }

        //shows only male samples
        public IActionResult OnPostMales()
        {

            // Opens a db connection using localhost database connection.Could also have used 127.0.0.1
            String str = @"server=localhost; database=MICE; userid=root; password=TSEGroup34;";
            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            try //To open localhost database and present a query
            {
                //Create a object with 'str' connection values passed. This uses the inbuilt library of MySql which is required
                conn = new MySqlConnection(str);
                conn.Open(); //opens the database connection
                Console.WriteLine("Localhost MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Assigns a query to cmdText which will be passed to the MySql database
                //Returns URLs of all female mice where the patient_id's match (because data is in 2 tables)
                string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE patient_sex = 'M');";

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to an array that will be passed to client side
                int i = 0;
                string[] arrayURL = new string[25];
                while (reader.Read())
                {
                    //Console.WriteLine(reader.GetString(0));
                    arrayURL[i] = reader.GetString(0);
                    i++;
                }

                ViewData["DICOMArrayList"] = arrayURL;

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

            return Page();
        }

        //shows only male samples
        public IActionResult OnPostReset()
        {

            // Opens a db connection using localhost database connection.Could also have used 127.0.0.1
            String str = @"server=localhost; database=MICE; userid=root; password=TSEGroup34;";
            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            try //To open localhost database and present a query
            {
                //Create a object with 'str' connection values passed. This uses the inbuilt library of MySql which is required
                conn = new MySqlConnection(str);
                conn.Open(); //opens the database connection
                Console.WriteLine("Localhost MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Assigns a query to cmdText which will be passed to the MySql database
                //Returns URLs of all female mice where the patient_id's match (because data is in 2 tables)
                string cmdText = @"
                SELECT urlString FROM MICE.url;";

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to an array that will be passed to client side
                int i = 0;
                string[] arrayURL = new string[25];
                while (reader.Read())
                {
                    //Console.WriteLine(reader.GetString(0));
                    arrayURL[i] = reader.GetString(0);
                    i++;
                }

                ViewData["DICOMArrayList"] = arrayURL;

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

            return Page();
        }
    }

}
