using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    [Authorize(Roles = SD.AdministratorRole)]
    public class PhotoCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PhotoCategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }




        [HttpGet]
        public IActionResult Get()
        {
            //Puts all the data from the table into a Json string
            return Json(new { data = _unitOfWork.PhotoCategory.GetAll() });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var objFromDb = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == Id);

                //Check category exists
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }


                //Get all Albums in Category
                IEnumerable<PhotoAlbum> AlbumList = new List<PhotoAlbum>();
                AlbumList = _unitOfWork.PhotoAlbum.GetAll(a => a.PhotoCategoryId == Id);

                //Remove Photos for those Albums
                foreach (PhotoAlbum album in AlbumList)
                {
                    //Get all Photos in Album
                    IEnumerable<Photo> PhotoList = new List<Photo>();
                    PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == Id);

                    //Delete Photo file and database record
                    if (PhotoList != null)
                    {
                        foreach (Photo photo in PhotoList)
                        {
                            //This concatonates the path of wwwroot with the image path from database.
                            //Remove the slash from the image path
                            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, photo.Image.TrimStart('\\'));
                            var thumbPath = Path.Combine(_hostingEnvironment.WebRootPath, photo.Thumb.TrimStart('\\'));
                            //Remove the file from wwwroot before removing path in database.
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                            //Remove the file from wwwroot before removing path in database.
                            if (System.IO.File.Exists(thumbPath))
                            {
                                System.IO.File.Delete(thumbPath);
                            }
                        }

                        //Delete Photo database records
                        _unitOfWork.Photo.RemoveRange(PhotoList);
                    }
                }

                //Delete Album database records
                _unitOfWork.PhotoAlbum.RemoveRange(AlbumList);


                //Delete Category database record
                _unitOfWork.PhotoCategory.Remove(objFromDb);

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            return Json(new { success = true, message = "Delete Successful." });
        }
    }
}
