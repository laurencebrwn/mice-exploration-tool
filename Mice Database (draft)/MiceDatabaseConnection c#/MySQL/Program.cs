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
using MySql.Data.MySqlClient; //library to connect to local MySql database. This reuires a NuGet (right click on Dependencies in solution folder > Manage NuGets

namespace MySQL
{
    class Program
    {
        static void Main(string[] args)
        {

            string cmdText; //string for mySql statement
            string exit = null; //string value for user input do-while loop

            //do-while loop for a user to keep selecting mySql queries until they decide to exit
            do
            {
                //Gets user input to choose their wanted MySql query serach parameter
                Console.WriteLine("\nPlease select what gender of search or 'e' for exit: m or f");
                string selection = Console.ReadLine(); //assigns user input to string

                //Assigns user input to switch-case statement
                switch (selection)//passes user input 'selection' to the switch statement
                {
                    case "m":
                        //MySql query for finding males 'M'
                        cmdText = @"
                        SELECT urlString FROM url WHERE patient_id IN 
                        (SELECT patient_id FROM mice WHERE patient_sex = 'M');";
                        PrintMySqlQuery(cmdText);//Passes MySql statement and prints 
                        break;

                    case "f":
                        //MySql query for finding females 'F'
                        cmdText = @"
                        SELECT urlString FROM url WHERE patient_id IN 
                        (SELECT patient_id FROM mice WHERE patient_sex = 'F');";
                        PrintMySqlQuery(cmdText); //Passes MySql statement and prints 
                        break;

                    case "e":
                        Console.WriteLine("You have exited the search.");
                        exit = selection;
                        break;

                    default:
                        Console.WriteLine("You have entered an incorrect value. Please try again");
                        break;
                }
            } while (exit != "e"); //Exits do-while loop when user selects 'e' at anytime

            Console.Read();
        }


        public static void PrintMySqlQuery(string cmdText)
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
                //Console.WriteLine("Localhost MySQL Database Connected"); //Shows this message if we have connected to the database

                //Creates object and passes all returned values to it
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();

                //Loops through the returned values and writes them to screen
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }
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
