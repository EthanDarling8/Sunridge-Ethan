using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;
using System;
using System.IO;
using System.Security.Claims;

namespace Sunridge.Pages.Owner.LostItem
{
    #region Authorization
    [Authorize] // Authorize only logged in Users.
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
        public string OwnerId { get; set; }
        #endregion
        #region OnGet(int? id)
        public IActionResult OnGet(int? id)
        {
            #region Owner Info
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
            {
                OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);      //Grab the OwnerId from the Login to use as a hidden field.
            }
            #endregion
            #region LostItem Object
            LostItemObj = new LostItemVM()
            {
                LostItem = new Models.LostItem(),
                OwnerList = _unitOfWork.Owner.GetOwnerListForDropDown()
            };
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
                // Upload and save image
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\lostItems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                LostItemObj.LostItem.Image = fileName + extension;

                _unitOfWork.LostItem.Add(LostItemObj.LostItem);
            }
            #endregion
            #region Update
            else
            {
                // Update
                var objFromDb = _unitOfWork.LostItem.Get(LostItemObj.LostItem.Id);
                if (files.Count > 0)
                {
                    // Upload and save image
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\lostItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var filePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
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