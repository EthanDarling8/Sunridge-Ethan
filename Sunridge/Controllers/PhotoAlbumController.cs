using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Sunridge.Utility;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotoAlbumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        // This gives us UnitOfWork and Repository functionality.
        public PhotoAlbumController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var objFromDb = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == Id);

                //Check album exists
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                //Get all photos in album
                IEnumerable<Models.Photo> PhotoList = new List<Models.Photo>();
                PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == Id);

                //Delete photo files and database record
                if(PhotoList != null)
                {
                    foreach (Models.Photo photo in PhotoList)
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

                    //Delete photo database record
                    _unitOfWork.Photo.RemoveRange(PhotoList);
                }                

                //Delete album
                _unitOfWork.PhotoAlbum.Remove(objFromDb);

                _unitOfWork.Save();

                return Json(new { success = true, message = "Delete Successful." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
        }
    }
}