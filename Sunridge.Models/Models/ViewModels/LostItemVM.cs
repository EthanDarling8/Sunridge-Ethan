using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LostItemVM
    {
        public LostItem LostItem { get; set; }

        //All the categories for a drop down list
        public IEnumerable<SelectListItem> OwnerList { get; set; }
    }
}
