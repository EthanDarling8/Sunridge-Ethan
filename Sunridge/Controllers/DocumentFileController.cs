using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentFileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DocumentFileController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }




        [HttpGet]
        public IActionResult Get(int categoryId)
        {
            return Json(new { data = _unitOfWork.DocumentFile.GetAll(null, null, "DocumentCategory") });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var objFromDb = _unitOfWork.DocumentFile.GetFirstOrDefault(s => s.Id == Id);

                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                //Delete file from wwwroot
                //This concatonates the path of wwwroot with the image path from database.
                //Remove the slash from the image path
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.File.TrimStart('\\'));
                //Remove the file from wwwroot before removing path in database.
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                //Queue delete file database record
                _unitOfWork.DocumentFile.Remove(objFromDb);

                //Save changes to database
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