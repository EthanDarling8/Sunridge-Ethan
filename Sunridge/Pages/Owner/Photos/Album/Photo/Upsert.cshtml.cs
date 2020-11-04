using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Owner.Photos.Album.Photo
{
    // **** ToDo **** setup user id check so only admin and creator can edit
    [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public UpsertModel(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnvironment,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }


        [BindProperty]
        public Models.Photo PhotoObj { get; set; }
        public int SelectedPhotoCategoryId { get; set; }
        public string OwnerId { get; set; }


        public IActionResult OnGet(int selectedPhotoAlbumId, int selectedPhotoCategoryId, int photoId)
        {
            PhotoObj = new Models.Photo();

            // **** ToDo **** Ensure this can only be accessed by an admin or the user that owns this picture.
            //Get Id of current user.
            OwnerId = _userManager.GetUserId(User);

            //Existing Photo (edit)
            if (photoId != 0)
            {
                PhotoObj = _unitOfWork.Photo.GetFirstOrDefault(p => p.Id == photoId);

                if (PhotoObj == null)
                {
                    //Returns a 404 error page.
                    return NotFound();
                }
            }


            //New photo, add it to the specified album
            if (photoId == 0)
            {
                //No PhotoAlbumId entered and new photo: invalid
                if (selectedPhotoAlbumId == 0)
                {
                    return NotFound();
                }            
            
                //Pass the album in
                PhotoObj.PhotoAlbumId = selectedPhotoAlbumId;
            }


            //Always preserve selected category
            SelectedPhotoCategoryId = selectedPhotoCategoryId;

            return Page();
        }




        public IActionResult OnPost(int photoId, int selectedPhotoAlbumId, int selectedPhotoCategoryId)
        {
            //Keep track of selected PhotoCategory
            SelectedPhotoCategoryId = selectedPhotoCategoryId;

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;


            // **** ToDo **** Add Thumb Conversion to these areas
            // store thumb link of last photo added to album
            // update thumb link when that photo is edited or removed

            //New Photo          
            if (photoId == 0)
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

                // **** ToDo **** Replace this with an actual thumb conversion
                PhotoObj.Thumb = @"\images\photoGallery\" + fileName + extension;

                _unitOfWork.Photo.Add(PhotoObj);


                //Make latest photo the thumbnail for the album
                // **** ToDo **** This needs to link to the converted thumb
                PhotoAlbum PhotoAlbumObj = new PhotoAlbum();
                PhotoAlbumObj = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);
                PhotoAlbumObj.Thumb = @"\images\photoGallery\" + fileName + extension;


                //Add PhotoAlbumId here because it is passed in via the route
                //for condition statements rather than as hidden inputs
                PhotoObj.PhotoAlbumId = selectedPhotoAlbumId;


                _unitOfWork.PhotoAlbum.Update(PhotoAlbumObj);                
            }

            //Existing Photo
            else
            {
                //Replace existing image
                //Check that an image exists
                var objFromDb = _unitOfWork.Photo.Get(photoId);
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


                    // **** ToDo **** Replace this with an actual thumb conversion
                    PhotoObj.Thumb = @"\images\photoGallery\" + fileName + extension;
                }
                else
                {
                    PhotoObj.Image = objFromDb.Image;
                    PhotoObj.Thumb = objFromDb.Thumb;
                }


                //Add PhotoId and PhotoAlbumId here because they are passed in via the route
                //for condition statements rather than as hidden inputs
                PhotoObj.Id = photoId;
                PhotoObj.PhotoAlbumId = selectedPhotoAlbumId;

                _unitOfWork.Photo.Update(PhotoObj);
            }


            _unitOfWork.Save();


            if (selectedPhotoCategoryId != 0)
            {
                return RedirectToPage("/Home/Photos/Index", new { SelectedPhotoAlbumId = selectedPhotoAlbumId, SelectedPhotoCategoryId = selectedPhotoCategoryId });
            }
            else
            {
                return RedirectToPage("/Home/Photos/Index", new { SelectedPhotoAlbumId = selectedPhotoAlbumId });
            }            
        }
    }
}
