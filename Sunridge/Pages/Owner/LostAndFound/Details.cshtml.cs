using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;
using System;

namespace Sunridge.Pages.Owner.LostAndFound
{
    #region Authorization
    [Authorize] // Authorize only logged in Users.
    #endregion
    public class Details : PageModel
    {
        #region Database Connection
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Details(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostEnvironment;
        }
        #endregion
        #region Variables
        [BindProperty]
        public LostItemVM LostItemObj { get; set; }
        public Models.Owner OwnerObj { get; set; }
        public string phoneNumber;
        public DateTime date;
        public String formattedDate;
        #endregion
        #region OnGet(int? id)
        public IActionResult OnGet(int? id)
        {
            #region LostItem Object
            LostItemObj = new LostItemVM()
            {
                LostItem = new Models.LostItem()
            };
            LostItemObj.LostItem = _unitOfWork.LostItem.GetFirstOrDefault(u => u.Id == id);
            if (LostItemObj == null)
            {
                return NotFound();
            }
            #endregion
            #region Owner Object
            OwnerObj = _unitOfWork.Owner.GetFirstOrDefault(u => u.Id == LostItemObj.LostItem.OwnerId);
            if (OwnerObj == null)
            {
                return NotFound();
            }
            #endregion
            #region Phone Number Formatting
            phoneNumber = String.Format("{0:(###) ###-####}", Convert.ToInt64(OwnerObj.PhoneNumber));
            #endregion
            #region Date Formatting
            date = (LostItemObj.LostItem.Date);
            formattedDate = date.ToString("MM/dd/yyyy");
            #endregion
            return Page();
        }
        #endregion
    }
}