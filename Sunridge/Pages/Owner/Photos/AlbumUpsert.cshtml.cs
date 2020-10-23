using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sunridge.Pages.Owner.Photos
{
    // **** ToDo **** [Authorize]
    public class UpsertModel : PageModel
    {
        public IActionResult OnGet(int? PhotoAlbumId)
        {
            // **** ToDo ****


            //New Album
            if(PhotoAlbumId == null)
            {
                
            }

            return Page();
        }
    }
}
