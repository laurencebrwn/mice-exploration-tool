using System;
using System.IO;

namespace JSON_Converter
{
    class JSONRead
    {
        static void readIn(string Filepath)
        {
            StreamReader Reader = new StreamReader(Filepath);
            try
            {
                using (Reader)
                {
                    string LineIn;
                    while((LineIn = Reader.ReadLine()) != null)
                    {
                        Console.WriteLine(LineIn);
                    }
                }
            }
            catch
            {

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
