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
    public class AssetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Asset.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {

            try
            {
                var objFromDb = _unitOfWork.Asset.GetFirstOrDefault(c => c.Id == Id);

                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                _unitOfWork.Asset.Remove(objFromDb);
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