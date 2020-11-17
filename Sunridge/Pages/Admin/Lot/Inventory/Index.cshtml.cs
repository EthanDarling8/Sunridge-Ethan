using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Lot.Inventory
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class Index : PageModel
    {
        public void OnGet()
        {

        }
    }
}