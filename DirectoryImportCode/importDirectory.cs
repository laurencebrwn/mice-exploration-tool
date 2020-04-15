
using System;
using System.Collections.Generic;
using System.IO;

namespace DirectoryImportCode
{
    class ImportDirectory
    {

        private string DirecotryPath;
        ImportDirectory(string Directory)
        {
            this.DirecotryPath = Directory;//Gets the directory
            FindFiles();
        }

        public List<Image> FindFiles()
        {
            string[] Dir = Directory.GetFiles(@DirecotryPath);//Allows for copy paste filepaths using Override.
            List<string> ImagePaths = new List<string>();//creates a list of filepaths
            List<string> TagPaths = new List<string>();//creates a list of filepaths
            foreach (string file in Dir)
            {//goes through directory finding all files
                string ext = Path.GetExtension(file);
                Console.WriteLine(file);
                if (ext != ".txt" || ext != ".json")
                {//sorts dependent upont file type
                    ImagePaths.Add(file);
                }
                else
                {
                    TagPaths.Add(file);
                }
            }
            List<Image> Images = SortImage(ImagePaths);//Sorts images of the same mouse into record.
            return Images;//returns a list of the Image object

        }

        public List<Image> SortImage(List<string> Files)
        {
            List<string> Filepaths = Files;
            List<Image> Images = new List<Image>();
            foreach (string File in Filepaths)
            {
                List<string> files = new List<string>();
                //string id = GetID(Filepaths[0]);
                files.Add(File);
                Image Mouse = new Image();
                Mouse.AddImage(files);
                Images.Add(Mouse);

            }
            return Images;
        }

        string GetID(string filepath)
        {
            int start = 0;
            string FileName = Path.GetFileNameWithoutExtension(filepath);
            for (int i = 0; i < FileName.Length; i++)
            {
                if (FileName[i] == '_')
                {
                    start = i + 1;
                    break;
                }
            }
            string ID = FileName.Substring(start);
            return ID;
        }

        void SortText()
        {//This method will handle all imported JSON files.

        }
    }
}