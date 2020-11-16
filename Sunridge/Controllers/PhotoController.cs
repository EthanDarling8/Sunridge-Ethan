using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PhotoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        // This gives us UnitOfWork and Repository functionality.
        public PhotoController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var objFromDb = _unitOfWork.Photo.GetFirstOrDefault(a => a.Id == Id);

                //Check photo exists
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                //Delete photo file and thumb file
                //This concatonates the path of wwwroot with the image path from database.
                //Remove the slash from the image path
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
                var thumbPath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Thumb.TrimStart('\\'));
                //Delete the file from wwwroot before removing path in database.
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                //Delete the file from wwwroot before removing path in database.
                if (System.IO.File.Exists(thumbPath))
                {
                    System.IO.File.Delete(thumbPath);
                }           


                //Delete photo from database
                _unitOfWork.Photo.Remove(objFromDb);

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