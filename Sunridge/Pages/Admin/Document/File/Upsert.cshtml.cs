using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sunridge.Pages.Admin.Document.File
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }


        [BindProperty]
        public DocumentFile DocumentFileObj { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> CategoryList { get; set; }


        public IActionResult OnGet(int fileId, int documentCategoryId)
        {
            //Alwayas Initialize
            DocumentFileObj = new DocumentFile();
            CategoryList = _unitOfWork.DocumentCategory.GetListForDropDown();


            //Adding new from documents page (selected category preserved)
            if (documentCategoryId != 0)
            {
                //Get category from documents page selection
                DocumentFileObj.DocumentCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == documentCategoryId);

                if (DocumentFileObj.DocumentCategory == null)
                {
                    return RedirectToPage("/Home/Documents/Index");
                }
                else
                {
                    DocumentFileObj.DocumentCategoryId = DocumentFileObj.DocumentCategory.Id;
                    //Default to today's date
                    DocumentFileObj.PublishedDate = DateTime.Today;
                }
            }
            //Existing (edit)
            else if (fileId != 0)
            {
                //Get existing
                DocumentFileObj = _unitOfWork.DocumentFile.GetFirstOrDefault(f => f.Id == fileId);

                if (DocumentFileObj == null)
                {
                    return RedirectToPage("/Home/Documents/Index");
                }
            }

            return Page();
        }




        public IActionResult OnPost(int fileId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            //New          
            if (fileId == 0)
            {
                //Upload new
                //Rename file to something unique to avoid duplicate file names.

                //string fileName = Guid.NewGuid().ToString();
                string fileName = Path.GetFileNameWithoutExtension(files[0].FileName) + "_" + Guid.NewGuid().ToString();


                //Set storage Path
                var uploads = Path.Combine(webRootPath, @"files\documents");
                //Keep original file extension (.jpg, etc) after Guid rename.
                var extension = Path.GetExtension(files[0].FileName);

                //Create storage path if it does not exist
                bool exists = Directory.Exists(uploads);
                if (!exists)
                    Directory.CreateDirectory(uploads);

                //Store the file
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                //Save string Path to file to database
                DocumentFileObj.File = @"\files\documents\" + fileName + extension;
                //Save extension to database
                DocumentFileObj.Extension = extension;

                //Queue for database
                _unitOfWork.DocumentFile.Add(DocumentFileObj);
            }

            //Existing file
            else
            {
                //Replace existing file
                //Check that a file exists
                var objFromDb = _unitOfWork.DocumentFile.Get(fileId);
                if (files.Count > 0)
                {
                    //Rename file to something unique to avoid duplicate file names.
                    string fileName = Guid.NewGuid().ToString();
                    //Set storage Path
                    //@ sign means to interpret string literally (includes slashes)
                    var uploads = Path.Combine(webRootPath, @"files\documents");
                    //Keep original file extension (.jpg, etc) after Guid rename.
                    var extension = Path.GetExtension(files[0].FileName);

                    //Check for an existing file.
                    if (objFromDb.File != null)
                    {
                        //This concatonates the path of wwwroot with the image path from database.
                        //Remove the slash from the image path
                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.File.TrimStart('\\'));
                        //Remove the file from wwwroot before removing path in database.
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    //Create storage path if it does not exist
                    bool exists = Directory.Exists(uploads);
                    if (!exists)
                        Directory.CreateDirectory(uploads);

                    //Store the updated image file
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    //Save string Path to file to database
                    DocumentFileObj.File = @"\files\documents\" + fileName + extension;
                    //Save extension to database
                    DocumentFileObj.Extension = extension;
                }
                //Keep existing file + extension
                else
                {
                    DocumentFileObj.File = objFromDb.File;
                    DocumentFileObj.Extension = objFromDb.Extension;
                }

                //Add fileId here because it is passed in via the route
                //for condition statements rather than as hidden inputs
                DocumentFileObj.Id = fileId;

                //Queue for database
                _unitOfWork.DocumentFile.Update(DocumentFileObj);
            }

            //Save to database
            _unitOfWork.Save();

            return RedirectToPage("Index");
        }
    }
}