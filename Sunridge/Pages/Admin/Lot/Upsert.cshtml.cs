using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// https://www.codewithmukesh.com/blog/select2-jquery-plugin-in-aspnet-core/
// https://select2.org/data-sources/ajax
// https://www.mikesdotnetting.com/article/265/asp-net-mvc-dropdownlists-multiple-selection-and-enum-support


namespace Sunridge.Pages.Admin.Lot
{
    #region Authorization
    [Authorize(Roles = SD.AdministratorRole)]
    #endregion
    public class Upsert : PageModel
    {
        #region Database Connection
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Upsert(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostEnvironment;
        }
        #endregion

        #region Variables
        [BindProperty]
        public LotVM LotObj { get; set; }
        #endregion

        #region OnGet(int? id)
        public IActionResult OnGet(int? id)
        {
            LotObj = new LotVM()
            {
                Lot = new Models.Lot(),
            };

            if (id != null)
            {
                LotObj.Lot = _unitOfWork.Lot.GetFirstOrDefault(u => u.Id == id);
                if (LotObj.Lot == null)
                {
                    return NotFound();
                }
            }

            var OwnerList = _context.Owner.Select(o => new {
                OwnerId = o.Id,
                OwnerName = o.FullName
            }).ToList();

            var LotOwnerIds = _unitOfWork.Lot_Owner.GetAll(l => l.LotId == LotObj.Lot.Id).ToList();
            string[] LotOwners = new string[LotOwnerIds.Count];
            for (int c = 0; c < LotOwnerIds.Count; c++)
            {
                LotOwners[c] = LotOwnerIds[c].OwnerId.ToString();
            }
            LotObj.OwnersList = new MultiSelectList(OwnerList, "OwnerId", "OwnerName", LotOwners);

            var InventoryIds = (from li in _unitOfWork.Lot_Inventory.GetAll(l => l.LotId == LotObj.Lot.Id) select new { li.InventoryId }).ToList();
            LotObj.InventoryList = _unitOfWork.Inventory.GetAll().ToList();
            int i = 0;
            foreach (var item in LotObj.InventoryList)
            {
                foreach (var lotInv in InventoryIds)
                {
                    if (lotInv.InventoryId == item.Id)
                    {
                        LotObj.InventoryList[i].IsChecked = true;
                    }
                }
                i++;
            }

            return Page();
        }
        #endregion

        #region OnPost()
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IList<Lot_Owner> newOwners = new List<Lot_Owner>();
            IList<Lot_Owner> RemovedOwners = new List<Lot_Owner>();

            IList<Lot_Inventory> NewInventory = new List<Lot_Inventory>();
            IList<Lot_Inventory> RemovedInventory = new List<Lot_Inventory>();

            // create initial list of new Owners
            if(LotObj.Owners != null)
            {
                foreach (var selOwnerID in LotObj.Owners)
                {
                    newOwners.Add(new Lot_Owner() { OwnerId = selOwnerID });
                }
            }
            // create initial list of new Inventory
            if (LotObj.Inventory != null)
            {
                foreach (var selInvID in LotObj.Inventory)
                {
                    NewInventory.Add(new Lot_Inventory() { InventoryId = selInvID });
                }
            }

            if (LotObj.Lot.Id != 0)
            {
                var objFromDb = _unitOfWork.Lot.Get(LotObj.Lot.Id);
                _unitOfWork.Lot.Update(LotObj.Lot);

                // find removed owners
                var LotOwners = _unitOfWork.Lot_Owner.GetAll(o => o.LotId == LotObj.Lot.Id).ToList();
                if (LotObj.Owners == null)
                {
                    RemovedOwners = LotOwners;
                }
                else
                {
                    foreach (var selOwnerID in LotObj.Owners)
                    {
                        RemovedOwners = LotOwners.Where(lo => lo.OwnerId != selOwnerID).ToList();
                    }
                }
                _unitOfWork.Lot_Owner.RemoveRange(RemovedOwners);
                // find new owners
                foreach (var ownerObj in LotOwners)
                {
                    newOwners = newOwners.Where(lo => lo.OwnerId != ownerObj.OwnerId).ToList();
                }

                // find removed inventory
                var LotInventory = _unitOfWork.Lot_Inventory.GetAll(o => o.LotId == LotObj.Lot.Id).ToList();
                if (LotObj.Inventory == null)
                {
                    RemovedInventory = LotInventory;
                }
                else
                {
                    foreach (var selInvID in LotObj.Inventory)
                    {
                        RemovedInventory = LotInventory.Where(li => li.InventoryId != selInvID).ToList();
                    }
                }
                _unitOfWork.Lot_Owner.RemoveRange(RemovedOwners);
                // find new inventory
                foreach (var invObj in LotInventory)
                {
                    NewInventory = NewInventory.Where(li => li.InventoryId != invObj.InventoryId).ToList();
                }
            }
            else
            {
                _unitOfWork.Lot.Add(LotObj.Lot);
            }
            _unitOfWork.Save();

            foreach (var item in newOwners)
            {
                item.LotId = LotObj.Lot.Id;
                _unitOfWork.Lot_Owner.Add(item);
            }

            foreach (var item in NewInventory)
            {
                item.LotId = LotObj.Lot.Id;
                _unitOfWork.Lot_Inventory.Add(item);
            }
            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
        #endregion
    }
}