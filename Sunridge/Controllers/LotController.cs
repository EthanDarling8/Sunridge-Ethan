using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public LotController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {

            ////Lot_Owners and Lot_Inventory
            //IEnumerable<Lot_Owner> lowner = _unitOfWork.Lot_Owner.GetAll(includeProperties: "Lot");
            //IEnumerable<Lot_Inventory> linv = _unitOfWork.Lot_Inventory.GetAll(includeProperties: "Lot");


            //LotOwnerInvObj = new LotOwnerInvVM
            //{
            //    LotInventory = linv,
            //    LotOwners = lowner
            //};

            //IQueryable<EnrollmentDateGroup> data =


            //data = new LotVM()
            //{
            //    Lots = _unitOfWork.Lot.GetAll(),
            //    Inventories = (IEnumerable<string>)(from l in _unitOfWork.Lot.GetAll()
            //                  join li in _unitOfWork.Lot_Inventory.GetAll() on l.Id equals li.LotId
            //                  join i in _unitOfWork.Inventory.GetAll() on li.InventoryId equals i.Id
            //                  select new
            //                  {
            //                      i.ItemName
            //                  }),
            //    Owners = (IEnumerable<string>)(from l in _unitOfWork.Lot.GetAll()
            //                  join lo in _unitOfWork.Lot_Owner.GetAll() on l.Id equals lo.LotId
            //                  join o in _unitOfWork.Owner.GetAll() on lo.OwnerId equals o.Id
            //                  select new
            //                  {
            //                      o.FullName
            //                  }),

            //};
            //var data = from l in _unitOfWork.Lot.GetAll()
            //           join li in _unitOfWork.Lot_Inventory.GetAll() on l.Id equals li.LotId
            //           join lo in _unitOfWork.Lot_Owner.GetAll() on l.Id equals lo.LotId
            //           select new
            //           {
            //               l.Id,
            //               l.LotNumber,
            //               l.TaxId,
            //               l.Address,
            //               li.InventoryId,
            //               lo.OwnerId
            //           };

        IEnumerable<object> RawSqlResult = null;
        using (_context)
                    {
                        RawSqlResult = _context.Lot.FromSqlRaw("SELECT Lot.Id, Lot.LotNumber, Lot.Address, Lot.Taxid, Query1.Lot_Inventory as [Lot_Inventory], Query2.Lot_Owner as [Lot_Owner] " +
        " FROM Lot " +
        " LEFT JOIN(SELECT LotNumber, STRING_AGG(ItemName, ', ') AS Lot_Inventory " +
                    " FROM Lot " +
                    " LEFT JOIN Lot_Inventory ON Lot_Inventory.LotId = Lot.Id " +
                    " LEFT JOIN Inventory ON Lot_Inventory.InventoryId = Inventory.Id " +
                    " GROUP BY LotNumber) Query1 ON Lot.LotNumber = Query1.LotNumber " +
        " LEFT JOIN(SELECT LotNumber, STRING_AGG(FirstName + ' ' + LastName, ', ') AS Lot_Owner " +
                    " FROM Lot " +
                    " LEFT JOIN Lot_Owner ON Lot_Owner.LotId = Lot.Id " +
                    " LEFT JOIN AspNetUsers ON Lot_Owner.OwnerId = AspNetUsers.Id " +
                    " GROUP BY LotNumber) Query2 ON Lot.LotNumber = Query2.LotNumber " +
        " ORDER BY Lot.LotNumber asc ").ToList();
                    }

            return Json(new { RawSqlResult });
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
