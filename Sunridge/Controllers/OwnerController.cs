using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Json(new { data = _unitOfWork.Owner.GetAll() });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _unitOfWork.Owner.GetFirstOrDefault(o => o.Id == id);
            
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking / Unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                // User is currently locked out so unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                // Lock user
                objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }

            _unitOfWork.Save();
            return Json(new { success = true, message = "Lock/Unlock Successful" });
        }
    }
}