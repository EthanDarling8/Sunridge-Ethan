using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.Models;

namespace Sunridge.Pages.Admin.KeyHistory
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Models.Lot Lot { get; set; }

        public Lot_Owner LotOwner { get; set; }

        public Models.Owner Owner { get; set; }
   
        [BindProperty]
        public IEnumerable<KeyLot> KeyLots { get; set; }

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            Lot = _unitOfWork.Lot.GetFirstOrDefault(l => l.Id == id);
            LotOwner = _unitOfWork.Lot_Owner.GetFirstOrDefault(lo => lo.LotId == id);

            if (LotOwner != null)
            {
                Owner = _unitOfWork.Owner.GetFirstOrDefault(o => o.Id == LotOwner.OwnerId);
            }
            else
            {
                Owner = new Models.Owner { FirstName = "No", LastName = "Owner" };
            }
          
            KeyLots = _unitOfWork.KeyLot.GetAll(kl => kl.LotId == id, null, "Key,Lot");
        }

        public IActionResult OnPost()
        {

            foreach (var keyLot in KeyLots)
            {

                if (keyLot.Issued == false) // If the Issued field is false, they key is being returned, so it needs a return date
                {
                    keyLot.ReturnDate = DateTime.Today;
                }

                _unitOfWork.KeyLot.Update(keyLot);
            }       

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}