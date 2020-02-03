using System;
using System.IO;
using System.Collections;

namespace JSON_Converter
{
    class JSONRead
    {
        static string[][] readIn(string Filepath)
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
            for(int i = 0; i<UnSortedArray.Length; i++){//iterates through
                Split = UnSortedArray[i].Split(":");//Splits it based upon the : character
                SortedArray[i][0] = Split[0];//Stored in new array
                SortedArray[i][1] = Split[1];
            }

            return SortedArray;
        }

        static string[][] RemoveUneeded(string[][] Input){
            string[] NeededTags = {"biological_sample_id","phenotyping_center","date_of_birth","sex","age_in_weeks","weight (g)","biological_sample_group",
            "gene_symbol","gene_accession_id","zygosity", "parameter_name", "observation_type","category","download_url","jpeg_url","biological_sample_id",
            "parameter_name"};
             ArrayList Info = new ArrayList();//ArrayList used to account for input size
            int x_length = Input.GetLength(0);
            for(int i = 0; i<x_length; i++){
                foreach (string Tag in NeededTags)
                {
                    if(Input[i][0].Contains(Tag)){
                        Info.Add(Input[i][0]);
                    }
                }
            }

            int SortedLenth =  Info.Count/NeededTags.GetLength();
            string[][] InfoArray = new string[NeededTags.GetLength()][SortedLenth];

            for(int i = 0; i<Info.Count; i+=8){

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
