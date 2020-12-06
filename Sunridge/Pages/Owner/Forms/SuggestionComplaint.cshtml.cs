using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Owner.Forms
{
    [Authorize]
    public class SuggestionComplaintModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Models.Owner> _userManager;
        public SuggestionComplaintModel(IUnitOfWork unitOfWork, UserManager<Models.Owner> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [BindProperty]
        public Models.Forms FormsObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            FormsObj = new Models.Forms();
            FormsObj.Owner = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            FormsObj.DateCreated = DateTime.Now;

            if (id != null)
            {
                FormsObj = _unitOfWork.Forms.GetFirstOrDefault(includeProperties: "Owner", filter: c => c.Id == id);
                if (FormsObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var UserObj = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

            if (FormsObj.Id == 0)
            {
                FormsObj.OwnerId = UserObj.Id;
                FormsObj.FormType = Models.FormType.SuggestionComplaint;
                FormsObj.DateCreated = DateTime.Now;
                _unitOfWork.Forms.Add(FormsObj);
                _unitOfWork.Save();
            }
            else
            {
                var objFromDb = _unitOfWork.Forms.Get(FormsObj.Id);
                _unitOfWork.Forms.Update(FormsObj);
            }

            return RedirectToPage("/Owner/Dashboard/Index");
        }
    }
}
