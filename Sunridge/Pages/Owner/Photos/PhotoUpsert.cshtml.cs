using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sunridge.Pages.Owner.Photos
{
    // **** ToDo **** [Authorize]
    public class PhotoUpsertModel : PageModel
    {
        public IActionResult OnGet(int? PhotoAlbumId)
        {
            // **** ToDo ****

            //No PhotoAlbumId entered: invalid
            if (PhotoAlbumId == null)
            {
                // **** ToDo ****
            }

            return Page();
        }
    }
}
