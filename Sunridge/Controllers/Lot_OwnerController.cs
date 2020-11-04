using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Lot_OwnerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public Lot_OwnerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Inventory.GetAll() });
        }

        // **** ToDo Setup Post Method? ****
    }
}