using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Event
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class IndexModel : PageModel
    {

    }
}
