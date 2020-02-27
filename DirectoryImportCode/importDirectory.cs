using System;
using System.IO;
using System.Collections.Generic;

namespace DirectoryImportCode{
    class ImportDirectory{
        
        private string DirecotryPath;
        ImportDirectory(string Directory)
        {
            this.DirecotryPath = Directory;//Gets the directory
            FindFiles();
        }

        public void FindFiles(){
            string[] Dir = Directory.GetFiles(@DirecotryPath);//Allows for copy paste filepaths using Override.
            List<string> ImagePaths = new List<string>();//creates a list of filepaths
            List<string> TagPaths = new List<string>();//creates a list of filepaths
            foreach(string file in Dir){//goes through directory finding all files
                string ext = Path.GetExtension(file);
                Console.WriteLine(file);
                 if( ext != ".txt" || ext !=".json"){//sorts dependent upont file type
                     ImagePaths.Add(file);
                 }else{
                    TagPaths.Add(file); 
                 }
            }  
             List<Image> Images = SortImage(ImagePaths);//Sorts images of the same mouse into record.
             //From here pass to code for database step.  

             foreach(Image mouse in Images){
                 List<string> files = mouse.GetImages();
                 foreach(string im in files){
                     Console.WriteLine(im);
                 }
             }
        }

        public List<Image> SortImage(List<string> Files){
            List<string> Filepaths = Files;
            List<Image> Images = new List<Image>();
            foreach(string File in Filepaths){
                Console.WriteLine("a");
                List<string>files = new List<string>();
                Console.WriteLine("b");
                //string id = GetID(Filepaths[0]);
                files.Add(File);
                Image Mouse = new Image();
                Mouse.AddImage(files);
                Images.Add(Mouse);
                Console.WriteLine("e");
            }
            // for this step you need to match substring after a _ and then remove from List. 
            //4. Continue until no more matching is done, removing all the matching from list.
            //5. Repeat until list empty
            return Images;
        }

        string GetID(string filepath){
            int start = 0;
            string FileName = Path.GetFileNameWithoutExtension(filepath);
            for(int i = 0; i<FileName.Length; i++){
                if(FileName[i] == '_'){
                    start = i+1;
                    break;
                }
            }
            string ID = FileName.Substring(start);
            return ID;
        }

        void SortText(){//This method will handle all imported JSON files.

        }

        public static void Main(string[] args){
            Console.WriteLine("Please enter the address of your directory");
            string dirPath = Console.ReadLine();
            ImportDirectory imp = new ImportDirectory(dirPath);
            Console.ReadLine();


        }
    }
}
    
