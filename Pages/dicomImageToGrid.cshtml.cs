﻿using System;
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

            //Shows all images in the databse as a defult GET request
            OnPostViewAll();
        }

        //show only female samples
        public IActionResult OnPostFemales()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE patient_sex = 'F');";

            MySqlConnection(cmdText);

            return Page();
        }

        //show only male examples
        public IActionResult OnPostMales()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE patient_sex = 'M');";

            MySqlConnection(cmdText);

            return Page();
            //return Redirect("Index"); //Directs to a new page where the result can be shown
        }

        //show only ICS Centre
        public IActionResult OnPostCentre()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE phenotyping_center = 'ICS');";

            MySqlConnection(cmdText);

            return Page();
        }

        //show only Gene RAB15
        public IActionResult OnPostGene()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE patient_gene = 'Rab15');";

            MySqlConnection(cmdText);

            return Page();
        }


        //show all images in MySql database
        public IActionResult OnPostViewAll()
        {

            string cmdText = @"
            SELECT urlString FROM MICE.url;";

            MySqlConnection(cmdText);

            return Page();
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
                Console.WriteLine("Localhost MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Assigns a query to cmdText which will be passed to the MySql database
                //Returns URLs of all female mice where the patient_id's match (because data is in 2 tables)
                //string cmdText = @"
                //SELECT urlString FROM MICE.url;";

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

    }

}
