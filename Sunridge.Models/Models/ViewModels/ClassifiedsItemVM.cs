using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedsItemVM
    {
        public ClassifiedsItem ClassifiedsItem { get; set; }
        public IEnumerable<SelectListItem> ClassifiedsCategoryList { get; set; }
        //public IEnumerable<SelectListItem> ClassifiedsSubcategoryList { get; set; }
    }
}
