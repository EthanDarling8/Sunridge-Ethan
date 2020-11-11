using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;
using System;
using System.IO;

namespace Sunridge.Pages.Admin.Lot
{
    #region Authorization
    //[Authorize(Roles = SD.AdministratorRole)] // Authorize use for only Administrators
    #endregion
    public class Upsert : PageModel
    {
        #region Database Connection
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Upsert(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostEnvironment;
        }
        #endregion
        #region Variables
        [BindProperty]
        public LotVM LotObj { get; set; }
        #endregion
        #region OnGet(int? id)
        public IActionResult OnGet(int? id)
        {
            #region Lot Object
            LotObj = new LotVM()
            {
                Lot = new Models.Lot(),
                InventoryList = _unitOfWork.Inventory.GetInventoryList() // Inventory List to create Inventory Checkboxes dynamically.
            };
            if (id != null)
            {
                LotObj.Lot = _unitOfWork.Lot.GetFirstOrDefault(u => u.Id == id);
                if (LotObj == null)
                {
                    return NotFound();
                }
            }
            #endregion
            return Page();                                                      //Refreshes page onGet if there's no id passed in
        }
        #endregion
        #region OnPost()
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
        #endregion
    }
}