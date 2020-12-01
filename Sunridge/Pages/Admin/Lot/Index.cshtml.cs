using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Lot
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class Index : PageModel
    {
        public void OnGet()
        {

        }
    }
}