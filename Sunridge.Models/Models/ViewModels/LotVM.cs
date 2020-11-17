using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LotVM
    {
        public IEnumerable<Lot> Lots { get; set; }

        // The Fields needed to display the Lot Index Page.
        public string LotNumber { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> Owners { get; set; }
        public string TaxId { get; set; }
        public IEnumerable<string> Inventories { get; set; }
    }
}
