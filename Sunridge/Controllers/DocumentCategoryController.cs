using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.DocumentCategory.GetAll() });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {

            // **** To Do **** Delete needs to remove all files, sections, section text in that category


            var objFromDb = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _unitOfWork.DocumentCategory.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful." });
        }
    }
}