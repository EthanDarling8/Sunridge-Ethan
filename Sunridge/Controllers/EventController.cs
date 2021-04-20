using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        // This gives us UnitOfWork and Repository functionality.
        public EventController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }




        [HttpGet]
        public IActionResult Get()
        {
            //Puts all the data from the table into a Json string
            return Json(new { data = _unitOfWork.Event.GetAll() });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var objFromDb = _unitOfWork.Event.GetFirstOrDefault(a => a.Id == Id);

                //Check photo exists
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                //Delete image file
                //This concatonates the path of wwwroot with the image path from database.
                //Remove the slash from the image path
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
                //Delete the file from wwwroot before removing path in database.
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }


                //Delete event from database
                _unitOfWork.Event.Remove(objFromDb);
                //MUST save before doing the thumb check!
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