using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LotVM
    {
        public Lot Lot { get; set; }
        public MultiSelectList OwnersList { get; set; }
        public string[] Owners { get; set; }
        public List<Inventory> InventoryList { get; set; }
        public int[] Inventory { get; set; }
    }
}
