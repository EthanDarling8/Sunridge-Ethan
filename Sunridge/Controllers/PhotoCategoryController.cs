using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class PhotoCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // This gives us UnitOfWork and Repository functionality.
        public PhotoCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            // **** To Do **** Delete needs to change all albums in that category to some default category (None)


            var objFromDb = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _unitOfWork.PhotoCategory.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful." });
        }
    }
}
