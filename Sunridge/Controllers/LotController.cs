using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LotController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        //public LotOwnerInvVM LotOwnerInvObj { get; set; }
        [BindProperty]
        public LotVM data { get; set; }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<object> RawSqlResult = null;
            using (var context = _context)
            {
                RawSqlResult = context.LotOwnerInvVM.FromSqlRaw("SELECT Lot.Id, Lot.LotNumber, Lot.Address, Lot.Taxid, Query1.Lot_Inventory as Lot_Inventory, Query2.Lot_Owner as Lot_Owner " +
"FROM Lot " +
"LEFT JOIN(SELECT LotNumber, STRING_AGG(ItemName, ', ') AS Lot_Inventory " +
            "FROM Lot " +
            "LEFT JOIN Lot_Inventory ON Lot_Inventory.LotId = Lot.Id " +
            "LEFT JOIN Inventory ON Lot_Inventory.InventoryId = Inventory.Id " +
            "GROUP BY LotNumber) Query1 ON Lot.LotNumber = Query1.LotNumber " +
"LEFT JOIN(SELECT LotNumber, STRING_AGG(FirstName + ' ' + LastName, ', ') AS Lot_Owner " +
            "FROM Lot " +
            "LEFT JOIN Lot_Owner ON Lot_Owner.LotId = Lot.Id " +
            "LEFT JOIN AspNetUsers ON Lot_Owner.OwnerId = AspNetUsers.Id " +
            "GROUP BY LotNumber) Query2 ON Lot.LotNumber = Query2.LotNumber " +
"ORDER BY Lot.LotNumber asc").ToList();

            }

            return Json(new { data = RawSqlResult.ToList() });
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

        [HttpGet]
        [Route("GetOwners")]
        public IActionResult Get(string term)
        {
            IEnumerable<Owner> data;

            if (!string.IsNullOrEmpty(term))
            {
                data = _unitOfWork.Owner.GetAll().Where(o => o.FirstName.ToLower().Contains(term.ToLower()));
            }
            else
            {
                data = _unitOfWork.Owner.GetAll();
            }
            return Json(new { data });
        }

    }

}
