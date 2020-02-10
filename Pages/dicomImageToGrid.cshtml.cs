using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace helloWorld.Pages
{
    public class dicomImageToGridModel : PageModel
    {
        private readonly ILogger<dicomImageToGridModel> _logger;

        public dicomImageToGridModel(ILogger<dicomImageToGridModel> logger)
        {
            _logger = logger;
        }

        public string Message { get; set; }
        public object HiddenField1 { get; set; }
        public object HiddenField2 { get; set; }

        public void OnGet()
        {

            Message = "Hello World";

            List<string> strList = new List<string>();
            strList.Add("str1");
            strList.Add("str2");
            strList.Add("str3");

            List<int> intList = new List<int>();
            intList.Add(1);
            intList.Add(2);
            intList.Add(3);


            HiddenField1 = Newtonsoft.Json.JsonConvert.SerializeObject(strList);
            HiddenField2 = Newtonsoft.Json.JsonConvert.SerializeObject(intList);
        }


    }
    
}
