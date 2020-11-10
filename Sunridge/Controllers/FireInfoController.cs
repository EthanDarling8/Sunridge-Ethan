using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Controllers {
    
    [Route("api/[controller]")]
    [ApiController]
    public class FireInfoController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FireInfoController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment) {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get() {
            return Json(new {data = _unitOfWork.FireInfo.GetAll()});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            try {
                var objFromDb = _unitOfWork.FireInfo.GetFirstOrDefault(unitOfWork => unitOfWork.Id == id);

                if (objFromDb == null) {
                    return Json(new {success = false, message = "Error while deleting"});
                }

                // Remove image (if exists)
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.File.TrimStart('\\'));
                if (System.IO.File.Exists(filePath)) {
                    System.IO.File.Delete(filePath);
                }

                _unitOfWork.FireInfo.Remove(objFromDb);
                _unitOfWork.Save();
            }
            catch (Exception e) {
                return Json(new {success = false, message = "Error while deleting"});
            }

            return Json(new {success = true, message = "Delete successfull"});
        }
    }
}