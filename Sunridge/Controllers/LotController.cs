using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LotController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public LotOwnerInvVM LotOwnerInvObj { get; set; }

        [HttpGet]
        public IActionResult Get()
        {

            //Lot_Owners and Lot_Inventory
            IEnumerable<Lot_Owner> lowner = _unitOfWork.Lot_Owner.GetAll(includeProperties: "Lot");
            IEnumerable<Lot_Inventory> linv = _unitOfWork.Lot_Inventory.GetAll(includeProperties: "Lot");


            LotOwnerInvObj = new LotOwnerInvVM
            {
                LotInventory = linv,
                LotOwners = lowner
            };


            return Json(new { LotOwnerInvObj });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.Lot.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                _unitOfWork.Lot.Remove(objFromDb);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = true, message = "Delete succcessful" });
        }
    }

}
