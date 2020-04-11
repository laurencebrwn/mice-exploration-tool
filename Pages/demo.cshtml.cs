﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace miceExplorationTool.Pages
{
    public class demoModel : PageModel
    {
        private readonly ILogger<demoModel> _logger;

        public demoModel(ILogger<demoModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        //show all images in db
        public IActionResult OnPostViewAll()
        {
            ViewData["MenuOption"] = "ViewAll";
            return Page();
        }

        //show only female samples
        public IActionResult OnPostFemales()
        {
            ViewData["MenuOption"] = "Females";
            return Page();
        }

        //show only Male samples
        public IActionResult OnPostMales()
        {
            ViewData["MenuOption"] = "Males";
            return Page();
        }

        public IActionResult OnPostFilter(string optionId, string optionCenter, string optionDob, string optionSex, string optionAge, string optionWeight, string optionGeneSymb, string optionGeneAccId, string optionZygosity, string optionParameter, string optionObvsType, string optionCategory)
        {
            ViewData["MenuOption"] = "UserFilter";
            if (optionId != "" && optionId != null)
            {
                ViewData["IdOption"] = optionId;
            }
            else
            {
                ViewData["IdOption"] = 'A';
            }
            ViewData["CenterOption"] = optionCenter;
            ViewData["DobOption"] = optionDob;
            ViewData["SexOption"] = optionSex;
            ViewData["AgeOption"] = optionAge;
            ViewData["WeightOption"] = optionWeight;
            ViewData["GeneSymbOption"] = optionGeneSymb;
            ViewData["GeneAccIdOption"] = optionGeneAccId;
            ViewData["ZygosityOption"] = optionZygosity;
            ViewData["ParameterOption"] = optionParameter;
            ViewData["ObvsTypeOption"] = optionObvsType;
            ViewData["CategoryOption"] = optionCategory;
            return Page();
        }

    }

}
