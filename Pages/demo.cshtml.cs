using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;



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

        public IActionResult OnPostFilter(string optionId, string optionCenter, string optionMinDob, string optionMaxDob, string optionSex, string optionMinAge, string optionMaxAge, string optionMinWeight, string optionMaxWeight, string optionGeneSymb, string optionGeneAccId, string optionZygosity, string optionParameter, string optionObvsType, string optionCategory)
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
            ViewData["DobMinOption"] = optionMinDob;
            ViewData["DobMaxOption"] = optionMaxDob;
            ViewData["SexOption"] = optionSex;
            ViewData["AgeMinOption"] = optionMinAge;
            ViewData["AgeMaxOption"] = optionMaxAge;
            ViewData["WeightMinOption"] = optionMinWeight;
            ViewData["WeightMaxOption"] = optionMaxWeight;
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
