//Author:   Garry Clawson
//Date:     02/20/2020 (Palindrone date!)  

//Credit: https://www.youtube.com/watch?v=bRmKK7nUMFo
//Credit: https://stackoverflow.com/questions/41789971/how-to-add-reference-to-assembly-in-visual-studio-mac

//Required: MySql for your machine; MySql workbench to upload your database; MySql.Data as a NuGet package (right click 'dependencies > search MySql.Data'

//>>>>>>> NOTES <<<<<<<<<
//You require a local MySql database and for ease of use MySql workbench to navigate the database for the below code to work. YOu will need to upload your database
//whihc in our instance will be the json file data which we are using to find the required images.

//The code connects to an instance of the database and returns values for the query to the console. IN this instance the query returns URLs of DICOM images
//if the Mouse is a Female.

using System;
using MySql.Data.MySqlClient;

namespace MySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Opens a db connection using localhost database connection. Could also have used 127.0.0.1
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

                //Loops through the returned values and writes them to screen
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }

            }
            catch(MySqlException errorMessage) //Prints exception if the connection cannot be opened (wrong password etc)
            {
                Console.WriteLine(errorMessage); 
            }
            finally //Once the try-ctach block is complete the connection is closed
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }

            Console.Read();
        }
    }
}
