using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.Pages.Owner.Dashboard
{
    [Authorize(Roles = "Owner")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly UserManager<Models.Owner> _userManager;
        
        public IEnumerable<Lot_Owner> LotOwners { get; set; }
        
        public Models.Owner Owner { get; set; }

        public IEnumerable<KeyLot> KeyLots { get; set; }

        public IndexModel(IUnitOfWork unitOfWork, UserManager<Models.Owner> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public void OnGet()
        {
            Owner = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            LotOwners = _unitOfWork.Lot_Owner.GetAll(lo => lo.OwnerId == Owner.Id, null, "Lot,Owner");
            KeyLots = _unitOfWork.KeyLot.GetAll(null, null, "Key,Lot");
        }
    }
}