using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Sunridge.Utility;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.AdministratorRole)]
    public class DocumentSectionTextController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentSectionTextController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.DocumentSectionText.GetAll(null, null, "DocumentSection") });
        }

        


        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var objFromDb = _unitOfWork.DocumentSectionText.GetFirstOrDefault(t => t.Id == Id);

                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                _unitOfWork.DocumentSectionText.Remove(objFromDb);

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