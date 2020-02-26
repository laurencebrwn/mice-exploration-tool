using System;
using System.Collections.Generic;

namespace DirectoryImportCode
{
    public class Image//Record of images
    {
        private List<string> ImageFilepaths = new List<string>();//stores the groups of images
        private List<string> TagHeaders = new List<string>();
        private List<string> TagInfo = new List<string>();

        public Image(){//creates a new object
            
        }

        public void AddImage(List<string> filepath){//sets the list of images to the image
            ImageFilepaths = filepath;
        }

        public void AddTagInfo(List<string> headers, List<string> body){//sets the info as 2 a
            this.TagHeaders = headers;
            this.TagInfo = body;
        }


        public List<string> GetImages(){//returns the list of images
            return this.ImageFilepaths;
        }

        public List<string> GetHeaders(){
            return this.TagHeaders;
        }

        public List<string> GetInfo(){
            return this.TagInfo;
        }
    }
}
