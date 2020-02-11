using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;

namespace helloWorld.Pages
{
    public class dicomImageToGridModel : PageModel
    {
        private readonly ILogger<dicomImageToGridModel> _logger;

        public dicomImageToGridModel(ILogger<dicomImageToGridModel> logger)
        {
            _logger = logger;
        }


        public void OnGet()
        {
       
            ViewData["DICOMArrayList"] = new string[] {"https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse1.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse2.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse3.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse4.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse5.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse6.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse7.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse8.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse1.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse2.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse3.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse4.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse5.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse6.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse7.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse8.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse1.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse2.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse3.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse4.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse5.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse6.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse7.dcm",
                "https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse8.dcm" };



        }



    }





}
