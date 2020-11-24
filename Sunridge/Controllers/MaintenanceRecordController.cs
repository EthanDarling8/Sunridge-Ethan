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
    public class MaintenanceRecordController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaintenanceRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            return Json(new { data = _unitOfWork.MaintenanceRecord.GetAll(mr => mr.AssetId == Id) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {

            try
            {
                var objFromDb = _unitOfWork.MaintenanceRecord.GetFirstOrDefault(c => c.Id == Id);

                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                _unitOfWork.MaintenanceRecord.Remove(objFromDb);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Delete Successful." });
            }
            catch (Exception)
            {
                return Json(new { success = true, message = "Error while deleting" });
            }

        }
    }
}