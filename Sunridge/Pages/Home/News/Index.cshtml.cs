using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sunridge.Pages.Home.News
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Search { get; set; }
        public void OnGet()
        {
        }
    }
}
