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
using Microsoft.AspNetCore.Mvc.Rendering;


namespace helloWorld.Pages
{
    [ValidateAntiForgeryToken]
    public class base64ToImageModel : PageModel
    {
        private readonly ILogger<base64ToImageModel> _logger;

        public base64ToImageModel(ILogger<base64ToImageModel> logger)
        {
            _logger = logger;
        }


        public void OnGet()
        {

        }

        public void OnPost()
        {

        }


        public IActionResult OnPostButton()
        {
            return RedirectToPage("Index");
        }

        //show only female samples
        public IActionResult OnPostPaw()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE parameter_name = 'paw');";

            MySqlConnection(cmdText);

            return Page();
        }

        //show only male samples
        public IActionResult OnPostBody()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE parameter_name = 'body');";

            MySqlConnection(cmdText);

            return Page();
        }

        public IActionResult OnPostHead()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE patient_id IN 
                (SELECT patient_id FROM mice WHERE parameter_name = 'head');";

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
