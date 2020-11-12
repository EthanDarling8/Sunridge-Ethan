using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LotVM
    {
        public Lot Lot { get; set; }

        // The Fields needed to display the Lot Index Page.
        public string LotNumber { get; set; }
        public string Address { get; set; }
        public IList<Owner> Owners { get; set; }
        public string TaxId { get; set; }
        public IList<Inventory> Inventories { get; set; }
    }
}
