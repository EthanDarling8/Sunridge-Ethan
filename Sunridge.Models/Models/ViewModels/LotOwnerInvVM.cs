using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class LotOwnerInvVM
    {
        public IEnumerable<Lot_Inventory> LotInventory { get; set; }
        public IEnumerable<Lot_Owner> LotOwners { get; set; }
    }
}
