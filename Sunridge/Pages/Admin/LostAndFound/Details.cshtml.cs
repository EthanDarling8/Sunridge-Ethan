using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;
using System;

namespace Sunridge.Pages.Admin.LostAndFound
{
    #region Authorization
    [Authorize(Roles = SD.AdministratorRole)] // Authorize use for only Administrators
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
        #region variables
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
            // Create the View Model
            LostItemObj = new LostItemVM()
            {
                LostItem = new Models.LostItem()
            };

            // Fill the View Model with the related information based on the Id of the Lost and Found Object
            LostItemObj.LostItem = _unitOfWork.LostItem.GetFirstOrDefault(u => u.Id == id);
            if (LostItemObj == null)
            {
                return NotFound();
            }
            #endregion
            #region Owner Object
            // Create the Owner Object to grab Information from the Owner Table used for display purposes.
            OwnerObj = _unitOfWork.Owner.GetFirstOrDefault(u => u.Id == LostItemObj.LostItem.OwnerId);
            if (OwnerObj == null)
            {
                return NotFound();
            }
            #endregion

            #region Phone Number Formatting
            phoneNumber = String.Format("{0:(###) ###-####}", Convert.ToInt64(OwnerObj.PhoneNumber)); //Format the PhoneNumber for display
            #endregion
            #region Date Formatting
            date = (LostItemObj.LostItem.Date); // Grab the date.
            formattedDate = date.ToString("MM/dd/yyyy"); // Format the date for display.
            #endregion


            return Page();
        }
        #endregion
    }
}