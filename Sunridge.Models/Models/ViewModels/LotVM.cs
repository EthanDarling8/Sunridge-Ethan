using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LotVM
    {
        public Lot Lot { get; set; }

        //All the categories for a drop down list
        public IEnumerable<SelectListItem> InventoryList { get; set; } // I need to get a List of Inventory Items so I can make checkboxes in the Upsert... Not sure how right now.
    }
}
