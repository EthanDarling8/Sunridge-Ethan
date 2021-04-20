using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Document.File
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class IndexModel : PageModel
    {

    }
}
