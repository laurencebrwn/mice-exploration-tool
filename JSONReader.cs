using System;
using System.IO;
using System.Collections;

namespace JSON_Converter
{
    class JSONRead
    {
        string[,] readIn(string Filepath)
        {
            ArrayList StringLists = new ArrayList();//ArrayList used to account for input size
            StreamReader Reader = new StreamReader(@Filepath);//Initiates reader for file location
            try//error handling
            {
                using (Reader)//using the reader class
                {
                    string LineIn;//holder string
                    while ((LineIn = Reader.ReadLine()) != null)//iterates until it reaches the end of the file
                    {
                        StringLists.Add(LineIn);//adds to array object
                    }
                    Console.WriteLine("Done");
                }
            }
            catch (IOException MissingInputError)//catches error
            {
                Console.WriteLine(MissingInputError);
                Console.WriteLine("Input File cannot be found!");
            }

            string[] UnSortedArray = (string[])StringLists.ToArray(typeof(string));//converts the object arrayList into a string array
            int le = UnSortedArray.Length;//gets length
            string[,] SortedArray = new string[le, 2];//generates a 2d array 

            for (int i = 0; i < UnSortedArray.Length; i++)
            {//iterates through
                Console.WriteLine(UnSortedArray[i]);
                if (UnSortedArray[i].Contains(':') == false)//Removes non 
                {
                    continue;
                }
                
                string[] Split = UnSortedArray[i].Split(':');//Splits it based upon the : character
                SortedArray[i, 0] = RemoveStrings(Split[0].Trim());//Stored in new array
                SortedArray[i, 1] = RemoveStrings(Split[1].Trim());//And removes Whitespace
            }

            return SortedArray;
        }

        string RemoveStrings(string input)//Iterates through the input, and removes unnecessary characters from input
        {
            string cleared = null;
            foreach (char element in input)
            {
                switch (element)//comparison
                {
                    case '"':
                        break;
                    case '{':
                        break;
                    case '[':
                        break;
                    case '}':
                        break;
                    case ']':
                        break;
                    case ',':
                        break;
                    default://if not one of the forbidden characters add to string
                        cleared = cleared + element;
                        break;
                }
            }
            return cleared;//return string
        }


        static void Main(string[] args)
        {
            JSONRead Reader = new JSONRead();//testing loop. Remove when implementing
            Console.WriteLine("Please enter address of the JSON file");
            string File = Console.ReadLine();
            string[,] test = Reader.readIn(File);
            for (int i = 0; i < test.GetLength(0); i++)
            {
                Console.WriteLine(i+" "+test[i, 0]);
                Console.WriteLine(i + " " + test[i, 1]);
                Console.WriteLine(i + " " + "");
            }
            Console.ReadKey();
        }
    }
}
