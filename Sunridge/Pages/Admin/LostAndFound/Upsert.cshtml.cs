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

namespace Sunridge.Pages.Admin.LostItem
{
    #region Authorization
    [Authorize(Roles = SD.AdministratorRole)] // Authorize use for only Administrators
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
        public LostItemVM LostItemObj { get; set; }
        #endregion
        #region OnGet(int? id)
        public IActionResult OnGet(int? id)
        {
            #region LostItem Object
            LostItemObj = new LostItemVM()
            {
                LostItem = new Models.LostItem(),
                OwnerList = _unitOfWork.Owner.GetOwnerListForDropdown()
            };
            LostItemObj.LostItem.Date = DateTime.Now; // Default a NEW Lost and Found Item's Date to the current DateTime.
            if (id != null)
            {
                LostItemObj.LostItem = _unitOfWork.LostItem.GetFirstOrDefault(u => u.Id == id);
                if (LostItemObj == null)
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
            #region Variables
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            LostItemObj.LostItem.Active = true;                                 // Set the Status of the newly created or edited Lost & Found Listing to true.
            LostItemObj.LostItem.ExpirationDate = DateTime.Now.AddDays(30);     // Set the Listing to expire in 30 days from creation or last edit.
            #endregion
            if (!ModelState.IsValid)
            {
                return Page();
            }
                #region Image Upload
                if (LostItemObj.LostItem.Id == 0)
                {
                if (files.Count > 0)
                {
                    // Upload and save image
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\lostItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    LostItemObj.LostItem.Image = fileName + extension;
                }
                else
                {
                    LostItemObj.LostItem.Image = "default.png";
                }
                _unitOfWork.LostItem.Add(LostItemObj.LostItem);
            }
            #endregion
            #region Update
            else
            {

                // Update
                var objFromDb = _unitOfWork.LostItem.Get(LostItemObj.LostItem.Id);
                objFromDb.Active = true;                                 // Set the Status of the newly created or edited Lost & Found Listing to true.
                objFromDb.ExpirationDate = DateTime.Now.AddDays(30);     // Set the Listing to expire in 30 days from creation or last edit.
                if (files.Count > 0)
                {
                    // Upload and save image
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\lostItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var filePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (LostItemObj.LostItem.Image != "default.png")
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    using (var fileStream =
                        new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    LostItemObj.LostItem.Image = fileName + extension;
                }
                else
                {
                    LostItemObj.LostItem.Image = objFromDb.Image;
                }

                _unitOfWork.LostItem.Update(LostItemObj.LostItem);

            }
            #endregion

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
        #endregion
    }
}