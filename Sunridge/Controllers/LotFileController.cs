using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LotFileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LotFileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Json(new { data = _unitOfWork.LotFile.GetAll()});
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Json(new { data = _unitOfWork.LotFile.GetAll(lf => lf.LotId == id) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LotFile.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.LotFile.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete succcessful" });
        }
    }
}