using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sunridge.Pages.Home.Documents
{
    public class IndexModel : PageModel
    {
        public int SelectedCategory { get; set; }

        public void OnGet(int selectedCategory)
        {
            SelectedCategory = selectedCategory;
        }
    }
}
