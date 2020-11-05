using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

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
            return Json(new { data = _unitOfWork.LotFile.GetAll() });
        }

        // **** ToDo Setup Post Method? ****
    }
}