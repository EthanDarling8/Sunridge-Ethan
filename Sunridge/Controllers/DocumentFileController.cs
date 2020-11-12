using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentFileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentFileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        [HttpGet]
        public IActionResult Get(int categoryId)
        {
            //**** ToDO **** filter by categoryId
            //return Json(new { data = _unitOfWork.DocumentSection.GetAll(c => c.DocumentCategoryId == categoryId) });
            return Json(new { data = _unitOfWork.DocumentFile.GetAll(null, null, "DocumentCategory") });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {

            // **** To Do **** Delete needs to remove all section texts associated with that section


            var objFromDb = _unitOfWork.DocumentFile.GetFirstOrDefault(s => s.Id == Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _unitOfWork.DocumentFile.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful." });
        }
    }
}