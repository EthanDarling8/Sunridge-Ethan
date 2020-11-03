using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class PhotoAlbumVM
    {
        public PhotoAlbum PhotoAlbum { get; set; }

        //All the categories for a drop down list
        public IEnumerable<SelectListItem> PhotoCategoryList { get; set; }
    }
}
