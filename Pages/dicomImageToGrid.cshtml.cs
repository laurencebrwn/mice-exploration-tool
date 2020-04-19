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
using Microsoft.AspNetCore.Cors;

namespace miceExplorationTool.Pages
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

        public void OnPost()
        {

        }

        //show only female samples
        public IActionResult OnPostFemales()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE id IN 
                (SELECT id FROM mice WHERE sex = 'female');";

            MySqlConnection(cmdText);

            return Page();
        }

        //show only male examples
        public IActionResult OnPostMales()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE id IN 
                (SELECT id FROM mice WHERE sex = 'male');";

            MySqlConnection(cmdText);

            return Page();
            //return Redirect("Index"); //Directs to a new page where the result can be shown
        }

        //show only ICS Centre
        public IActionResult OnPostCentre()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE id IN 
                (SELECT id FROM mice WHERE phenotyping_center = 'WTSI');";

            MySqlConnection(cmdText);

            return Page();
        }

        //show only Gene RAB15
        public IActionResult OnPostGene()
        {

            string cmdText = @"
                SELECT urlString FROM url WHERE id IN 
                (SELECT id FROM mice WHERE gene_symbol = 'Rab15');";

            MySqlConnection(cmdText);

            return Page();
        }


        //show all images in MySql database
        public IActionResult OnPostViewAll()
        {

            string cmdText = @"
            SELECT urlString FROM MICE.url;";

            MySqlConnection(cmdText);

            //populates all drop down menu parameters
            OnPostDropDowns();

            return Page();
        }

        //populate the drop downs
        public IActionResult OnPostViewDropDowns()
        {

            //populates all drop down menu parameters
            OnPostDropDowns();

            return Page();
        }



        //Presents user filter selection from drop downs and creates a MySql command to get images
        public IActionResult OnPostFilter(string optionId, string optionCenter, string optionDob, string optionSex, string optionAge, string optionWeight, string optionGeneSymb, string optionGeneAccId, string optionZygosity, string optionParameter, string optionObvsType, string optionCategory)
        {

            //ViewData["MenuOption"] = "UserFilter";
            //if (optionId != "" && optionId != null)
            //{
            //    ViewData["IdOption"] = optionId;
            //}
            //else
            //{
            //    ViewData["IdOption"] = 'A';
            //}

            //ViewData["DobOption"] = optionDob;
            //ViewData["SexOption"] = optionSex;
            //ViewData["AgeOption"] = optionAge;
            //ViewData["WeightOption"] = optionWeight;
            //ViewData["GeneSymbOption"] = optionGeneSymb;
            //ViewData["GeneAccIdOption"] = optionGeneAccId;
            //ViewData["ZygosityOption"] = optionZygosity;
            //ViewData["ParameterOption"] = optionParameter;
            //ViewData["CenterOption"] = optionCenter;
            //ViewData["ObvsTypeOption"] = optionObvsType;

            //ViewData["CategoryOption"] = optionCategory;


            Console.WriteLine("Sex selected: {0}:", optionSex);


            string cmdText = "SELECT urlString FROM url WHERE id IN" +
                "(SELECT id FROM mice WHERE " +
                "date_of_birth = '" + optionDob + "' AND " +
                "sex = '" + optionSex + "' AND " +
                "age_in_weeks = '" + optionAge + "' AND " +
                "weight = '" + optionWeight + "' AND " +
                "gene_symbol = '" + optionGeneSymb + "' AND " +
                "gene_accession_id = '" + optionGeneAccId + "' AND " +
                "zygosity = '" + optionZygosity + "' AND " +
                "parameter_name = '" + optionParameter + "' AND " +
                "phenotyping_center = '" + optionCenter + "' AND " +
                "observation_type = '" + optionObvsType + "');";

            MySqlConnection(cmdText);



            //populates all drop down menu parameters ready for the next search
            OnPostDropDowns();

            return Page();
        }


        //populate drop down menus
        public IActionResult OnPostDropDowns()
        {

            Console.WriteLine("This is the dropdowns section");

            string DobText = "USE MICE; SELECT date_of_birth FROM mice;";
            ViewData["DobOption"] = HeadersMySqlConnection(DobText);

            string sexText = "USE MICE; SELECT sex FROM mice;";
            ViewData["SexOption"] = HeadersMySqlConnection(sexText);


            string AgeText = "USE MICE; SELECT age_in_weeks FROM mice;";
            ViewData["AgeOption"] = HeadersMySqlConnection(AgeText);

            string WeightText = "USE MICE; SELECT weight FROM mice;";
            ViewData["WeightOption"] = HeadersMySqlConnection(WeightText);

            string IdText = "USE MICE; SELECT biological_sample_id FROM mice;";
            ViewData["IdOption"] = HeadersMySqlConnection(IdText);

            string GeneSymbText = "USE MICE; SELECT gene_symbol FROM mice;";
            ViewData["GeneSymbOption"] = HeadersMySqlConnection(GeneSymbText);

            string GeneAccIdText = "USE MICE; SELECT gene_accession_id FROM mice;";
            ViewData["GeneAccIdOption"] = HeadersMySqlConnection(GeneAccIdText);

            string ZygosityText = "USE MICE; SELECT zygosity FROM mice;";
            ViewData["ZygosityOption"] = HeadersMySqlConnection(ZygosityText);

            string ParameterText = "USE MICE; SELECT parameter_name FROM mice;";
            ViewData["ParameterOption"] = HeadersMySqlConnection(ParameterText);

            string ObvsTypeText = "USE MICE; SELECT observation_type FROM mice;";
            ViewData["ObvsTypeOption"] = HeadersMySqlConnection(ObvsTypeText);

            return Page();
        }


        //Main functon that connects to the ySql server and returns the reuqired URLs
        public List<string> HeadersMySqlConnection(string connection)
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
                //Console.WriteLine("MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to a list that will be passed to client side
                List<string> urlList = new List<string>();

                while (reader.Read())
                {
                    //Console.WriteLine("string: " + reader.GetString(0));

                    //checks if database value returned is null i.e. no record in associated id column for the URL)
                    //because the users does not have the image for that particular mouse in their image folder
                    if (!reader.IsDBNull(0))
                    {
                        urlList.Add(reader.GetString(0));
                    }
                    else
                    {
                        Console.WriteLine("No URL string associated with image id");
                    }

                }


                Console.WriteLine(String.Join("\n", urlList)); //prints headers list to be passed to front end

                return urlList;

                //ViewData["SexOption"] = urlList;

            }
            catch (MySqlException errorMessage) //Prints exception if the connection cannot be opened (wrong password etc)
            {
                Console.WriteLine(errorMessage);
                return null;
            }
            finally //Once the try-ctach block is complete the connection is closed
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }

        }





        //display user selected query images
        public IActionResult OnPostName(string centre, string sex)
        {


            //check string to make sure it does not contain an incorrect value



            //End of test by Chris

            string cmdText = "SELECT urlString FROM url WHERE id IN(SELECT id FROM mice WHERE phenotyping_center = '" + centre + "' AND sex = '" + sex + "');";

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
                //Console.WriteLine("MySQL Database Connected"); //If the database opens it presents this messsge. 

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to a list that will be passed to client side
                List<string> urlList = new List<string>();

                while (reader.Read())
                {
                    //Console.WriteLine("string: " + reader.GetString(0));

                    //checks if database value returned is null i.e. no record in associated id column for the URL)
                    //because the users does not have the image for that particular mouse in their image folder
                    if (!reader.IsDBNull(0))
                    {
                        urlList.Add(reader.GetString(0));
                    }
                    else
                    {
                        Console.WriteLine("No URL string associated with image id");
                    }

                }


                //Console.WriteLine(String.Join("\n", urlList)); //prints url list to be passed to front end

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
