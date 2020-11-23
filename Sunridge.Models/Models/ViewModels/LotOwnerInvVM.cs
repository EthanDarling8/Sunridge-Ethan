using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LotOwnerInvVM
    {
        // The Fields needed to display the Lot Index Page.
        public int id { get; set; }
        public string LotNumber { get; set; }
        public string Address { get; set; }
        public string Lot_Owner { get; set; }
        public string TaxId { get; set; }
        public string Lot_Inventory { get; set; }
    }
}
