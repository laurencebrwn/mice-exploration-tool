using System;
using System.IO;
using System.Collections;

namespace JSON_Converter
{
    class JSONRead
    {
        static void readIn(string Filepath)
        {
            ArrayList StringLists = new ArrayList();//ArrayList used to account for input size
            StreamReader Reader = new StreamReader(Filepath);//Initiates reader for file location
            try//error handling
            {
                using (Reader)//using the reader class
                {
                    string LineIn;//holder string
                    while((LineIn = Reader.ReadLine()) != null)//iterates until it reaches the end of the file
                    {
                        StringLists.Add(LineIn);//adds to array object
                    }
                    Console.WriteLine("Done");
                }
            }
            catch(IOException MissingInputError)//catches error
            {
                Console.WriteLine(MissingInputError);
                Console.WriteLine("Input File cannot be found!");
            }

            string[] UnSortedArray = (string[])StringLists.ToArray();//converts the object arrayList into a string array
            int i = UnSortedArray.Length;//gets length
            string[][] SortedArray = new string[i][1];//generates a 2d array 
            string[] Split = new string[2];
            for(int i = 0; i<UnSortedArray.Length; i++){
                Split = UnSortedArray[i].Split(":");
                SortedArray[i][0] = Split[0];
                SortedArray[i][1] = Split[1];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter address of the JSON file");
            string File = Console.ReadLine();
            string Filepath = @File;
            JSONRead.readIn(Filepath);
            Console.ReadKey();
        }
    }
}
