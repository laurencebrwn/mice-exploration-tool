using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft;
using MySql.Data.MySqlClient;
using System.Web;
using Microsoft.AspNetCore.Cors;


namespace miceExplorationTool.Pages
{

    public class chartsModel : PageModel
    {
        private readonly ILogger<chartsModel> _logger;

        public chartsModel(ILogger<chartsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //FilePath();
        }

        public void OnPost()
        {

        }
  
    }
}
