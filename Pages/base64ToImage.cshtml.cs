using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace helloWorld.Pages
{
    public class base64ToImageModel : PageModel
    {
        private readonly ILogger<base64ToImageModel> _logger;

        public base64ToImageModel(ILogger<base64ToImageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
