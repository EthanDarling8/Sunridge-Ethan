using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LotVM
    {
        public Lot Lot { get; set; }
        public Lot_Owner Lot_Owner { get; set; }
        public Lot_Inventory Lot_Inventory { get; set; }

        // The Drop Down Lists.
        public IEnumerable<SelectListItem> Owners { get; set; }
        public IEnumerable<SelectListItem> Inventories { get; set; }
    }
}
