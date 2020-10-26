using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Owner.Photos
{
    // **** ToDo **** [Authorize]
    public class PhotoUpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public PhotoUpsertModel(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnvironment,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }


        [BindProperty]
        public Photo PhotoObj { get; set; }

        public string ApplicationUserId { get; set; }


        public IActionResult OnGet(int? PhotoAlbumId, int? PhotoId)
        {
            PhotoObj = new Photo();

            // **** ToDo **** Ensure this can only be accessed the an admin or the user that owns this picture.
            //Get Id of current user.
            ApplicationUserId = _userManager.GetUserId(User);

            //Existing Photo (edit)
            if (PhotoId != null)
            {
                PhotoObj = _unitOfWork.Photo.GetFirstOrDefault(p => p.Id == PhotoId);

                if (PhotoObj == null)
                {
                    //Returns a 404 error page.
                    return NotFound();
                }
            }
            
            
            //PhotoAlbumId entered: invalid
            if (PhotoAlbumId == null)
            {
                return NotFound();
            }

            return Page();
        }




        public IActionResult OnPost(int PhotoId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;


            // **** ToDo **** Add Thumb Conversion to these areas

            //New Photo          
            if (PhotoId == 0)
            {
                //Upload new image
                //Rename file to something unique to avoid duplicate file names.
                string fileName = Guid.NewGuid().ToString();
                //Set storage Path
                //@ sign means to interpret string literally (includes slashes)
                var uploads = Path.Combine(webRootPath, @"images\photoGallery");
                //Keep original file extension (.jpg, etc) after Guid rename.
                var extension = Path.GetExtension(files[0].FileName);

                //Create storage path if it does not exist
                bool exists = Directory.Exists(uploads);
                if (!exists)
                    Directory.CreateDirectory(uploads);

                //Store the image file
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                //Save string Path to image file to database
                PhotoObj.Image = @"\images\photoGallery\" + fileName + extension;
                

                _unitOfWork.Photo.Add(PhotoObj);
            }

            //Existing Photo
            else
            {
                //Replace existing image
                //Check that an image exists
                var objFromDb = _unitOfWork.Photo.Get(PhotoId);
                if (files.Count > 0)
                {
                    //Rename file to something unique to avoid duplicate file names.
                    string fileName = Guid.NewGuid().ToString();
                    //Set storage Path
                    //@ sign means to interpret string literally (includes slashes)
                    var uploads = Path.Combine(webRootPath, @"images\photoGallery");
                    //Keep original file extension (.jpg, etc) after Guid rename.
                    var extension = Path.GetExtension(files[0].FileName);

                    //Check for an existing image.
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

                    //Save string Path to image file to database
                    PhotoObj.Image = @"\images\photoGallery\" + fileName + extension;
                }
                else
                {
                    PhotoObj.Image = objFromDb.Image;
                }

                _unitOfWork.Photo.Update(PhotoObj);
            }


            _unitOfWork.Save();

            //RedirectToPage allows variables to be passed in.
            return RedirectToPage("./Index");
        }
    }
}
