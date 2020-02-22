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


        public string Message
        {
            get;
            set;
        }


        public void OnGet()
        {
            Message = "Enter your message here";
        }

        public void OnPost()
        {
            Message = Request.Form[nameof(Message)];
        }


        public IActionResult OnPostButton()
        {
            return RedirectToPage("Index");
        }

        public IActionResult OnPostMouseClick()
        {
            return RedirectToPage("Index");
        }




    }
}
