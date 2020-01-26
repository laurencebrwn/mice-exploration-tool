using System;
using System.IO;
using System.Collections;

namespace JSON_Converter
{
    class JSONRead
    {
        static void readIn(string Filepath)
        {
            ArrayList StringLists = new ArrayList();
            StreamReader Reader = new StreamReader(Filepath);
            try
            {
                using (Reader)
                {
                    string LineIn;
                    while((LineIn = Reader.ReadLine()) != null)
                    {
                        StringLists.Add(LineIn);
                    }
                    Console.WriteLine("Done");
                }
            }
            catch(IOException MissingInputError)
            {
                Console.WriteLine(MissingInputError);
                Console.WriteLine("Input File cannot be found!");
            }

            string[] UnSortedArray = (string[])StringLists.ToArray();
            int i = UnSortedArray.Length;
            string[][] SortedArray = new string[i][i];
            string[] Split = new string[2];
            foreach(string info in UnSortedArray){
                Split = info.Split(":");
                
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
