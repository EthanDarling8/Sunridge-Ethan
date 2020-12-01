using System;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Pages.Owner.Photos.Album.Photo
{    
    [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<Models.Owner> _userManager;

        public UpsertModel(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnvironment,
            UserManager<Models.Owner> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }


        [BindProperty]
        public Models.Photo PhotoObj { get; set; }
        public PhotoAlbum SelectedPhotoAlbum { get; set; }
        public int SelectedPhotoCategoryId { get; set; }
        public string OwnerId { get; set; }
        public bool MyAlbums { get; set; }




        public IActionResult OnGet(int selectedPhotoAlbumId, int selectedPhotoCategoryId, int photoId, bool myAlbums = false)
        {
            //Used to pass selected categroy into page for button links to preserve selection
            SelectedPhotoCategoryId = selectedPhotoCategoryId;

            //Used to pass myAlbums into page for button links to preserve selection
            MyAlbums = myAlbums;


            //Get Id of current user.
            OwnerId = _userManager.GetUserId(User);

            PhotoObj = new Models.Photo();


            //Existing Photo (edit)
            //Ignores selectedPhotoAlbumId, uses existing database entry.
            if (photoId != 0)
            {
                PhotoObj = _unitOfWork.Photo.GetFirstOrDefault(p => p.Id == photoId);                

                if (PhotoObj == null)
                {
                    return RedirectToPage("/Home/Photos/Index", new { selectedPhotoCategoryId = selectedPhotoCategoryId, myAlbums = myAlbums });
                }

                SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == PhotoObj.PhotoAlbumId);
            }


            //New photo, add it to the selected album
            if (photoId == 0)
            {
                //No selectedPhotoAlbumId entered: invalid
                if (selectedPhotoAlbumId == 0)
                {
                    return RedirectToPage("/Home/Photos/Index", new { selectedPhotoCategoryId = selectedPhotoCategoryId, myAlbums = myAlbums });
                }


                SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);

                //Ensure selectedPhotoAlbumId is an existing album
                if (SelectedPhotoAlbum != null)
                {
                    //Pass the album in
                    PhotoObj.PhotoAlbumId = selectedPhotoAlbumId;
                }
                else
                {
                    return RedirectToPage("/Home/Photos/Index", new { selectedPhotoCategoryId = selectedPhotoCategoryId, myAlbums = myAlbums });
                }                
            }            


            //Only allow admins and creator to access            
            if (User.IsInRole(SD.AdministratorRole) || SelectedPhotoAlbum.OwnerId == OwnerId)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Home/Photos/Index", new { selectedPhotoCategoryId = selectedPhotoCategoryId, myAlbums = myAlbums });
            }
        }




        public IActionResult OnPost(int photoId, int selectedPhotoAlbumId, int selectedPhotoCategoryId, bool myAlbums = false)
        {
            //Keep track of selected PhotoCategory
            SelectedPhotoCategoryId = selectedPhotoCategoryId;

            //This needs to be here for new vs existing logic
            var files = HttpContext.Request.Form.Files;

            //New Photo being uploaded.        
            if (photoId == 0)
            {
                SaveImage(selectedPhotoAlbumId);

                _unitOfWork.Photo.Add(PhotoObj);
            }

            //Existing Photo being replaced.
            else
            {
                //Replace existing image
                //Check that an image exists
                var objFromDb = _unitOfWork.Photo.Get(photoId);
                if (files.Count > 0)
                {
                    //Check photo exists
                    if (objFromDb == null)
                    {
                        return RedirectToPage("/Home/Photos/Index", new { selectedPhotoAlbumId = selectedPhotoAlbumId, myAlbums = myAlbums });
                    }

                    //Remove existing image.
                    //This concatonates the path of wwwroot with the image path from database.
                    //Remove the slash from the image path
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
                    //Remove the file from wwwroot before removing path in database.
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }                    

                    //Remove existing thumb.                
                    //This concatonates the path of wwwroot with the image path from database.
                    //Remove the slash from the image path
                    var thumbPath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Thumb.TrimStart('\\'));
                    //Remove the file from wwwroot before removing path in database.
                    if (System.IO.File.Exists(thumbPath))
                    {
                        System.IO.File.Delete(thumbPath);
                    }                   


                    SaveImage(selectedPhotoAlbumId);

                    //Keep track of photo being updated.
                    PhotoObj.Id = photoId;

                    _unitOfWork.Photo.Update(PhotoObj);                    
                }

                //Existing Photo being kept.
                else
                {
                    //Keep track of photo being updated.
                    PhotoObj.Id = photoId;

                    PhotoObj.Image = objFromDb.Image;
                    PhotoObj.Thumb = objFromDb.Thumb;

                    _unitOfWork.Photo.Update(PhotoObj);
                }                
            }

            //Save changes to database.
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




        //Saves the uploaded image and a thumbnail of that image to wwwroot with link in database.
        void SaveImage(int selectedPhotoAlbumId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            int thumbWidth = 200;
            int thumbHeight = 200;


            //Upload image
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


            //Create then store the thumb file
            using (Stream stream = files[0].OpenReadStream())
            {
                Image image = Image.FromStream(stream);
                Image thumb = image.GetThumbnailImage(thumbWidth, thumbHeight, () => false, IntPtr.Zero);

                thumb.Save(Path.Combine(uploads, fileName + "_thumb" + extension));
            }


            //Save string Path to image file to database
            PhotoObj.Image = @"\images\photoGallery\" + fileName + extension;

            //Save string Path to thumb file to database
            PhotoObj.Thumb = @"\images\photoGallery\" + fileName + "_thumb" + extension;            


            //Make latest photo the thumbnail for the album
            PhotoAlbum PhotoAlbumObj = new PhotoAlbum();
            PhotoAlbumObj = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);
            PhotoAlbumObj.Thumb = @"\images\photoGallery\" + fileName + "_thumb" + extension;

            //Keep track of album photo belongs to.
            PhotoObj.PhotoAlbumId = selectedPhotoAlbumId;

            _unitOfWork.PhotoAlbum.Update(PhotoAlbumObj);
        }
    }
}
