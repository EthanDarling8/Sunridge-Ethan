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

namespace Sunridge.Pages.Admin.Event
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
        public Models.Event EventObj { get; set; }


        public IActionResult OnGet(int eventId)
        {
            //Alwayas Initialize
            EventObj = new Models.Event();


            //Existing (edit)
            if (eventId != 0)
            {
                //Get existing
                EventObj = _unitOfWork.Event.GetFirstOrDefault(e => e.Id == eventId);

                if (EventObj == null)
                {
                    return RedirectToPage("/Home/Calendar/Index");
                }
            }
            //New: default behavior if no eventId
            else
            {
                //Default to today's date
                EventObj.Start = DateTime.Today;
                EventObj.End = DateTime.Today;
            }
            

            return Page();
        }




        public IActionResult OnPost(int eventId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            //New          
            if (eventId == 0)
            {
                if (files.Count > 0)
                {
                    //Upload new
                    //Rename file to something unique to avoid duplicate file names.
                    //string fileName = Guid.NewGuid().ToString();
                    string fileName = Path.GetFileNameWithoutExtension(files[0].FileName) + "_" + Guid.NewGuid().ToString();

                    //Set storage Path
                    var uploads = Path.Combine(webRootPath, @"files\events");
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

                    //Save string Path to image to database
                    EventObj.Image = @"\files\events\" + fileName + extension;
                }

                //Queue for database
                _unitOfWork.Event.Add(EventObj);
            }
            //Existing image
            else
            {
                //Replace existing file
                //Check that a file exists
                var objFromDb = _unitOfWork.Event.Get(eventId);
                if (files.Count > 0)
                {
                    //Rename file to something unique to avoid duplicate file names.
                    string fileName = Guid.NewGuid().ToString();
                    //Set storage Path
                    //@ sign means to interpret string literally (includes slashes)
                    var uploads = Path.Combine(webRootPath, @"files\events");
                    //Keep original file extension (.jpg, etc) after Guid rename.
                    var extension = Path.GetExtension(files[0].FileName);

                    //Check for an existing file.
                    if (objFromDb.Image != null)
                    {
                        //This concatonates the path of wwwroot with the image path from database.
                        //Remove the slash from the image path
                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
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
                    EventObj.Image = @"\files\events\" + fileName + extension;
                }
                //Keep existing image
                else
                {
                    EventObj.Image = objFromDb.Image;
                }

                //Add fileId here because it is passed in via the route
                //for condition statements rather than as hidden inputs
                EventObj.Id = eventId;

                //Queue for database
                _unitOfWork.Event.Update(EventObj);
            }

            //Save to database
            _unitOfWork.Save();

            return RedirectToPage("Index");
        }
    }
}