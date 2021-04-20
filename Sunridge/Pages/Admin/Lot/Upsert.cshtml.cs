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

            if (LotObj.Lot.Id != 0)
            {
                var objFromDb = _unitOfWork.Lot.Get(LotObj.Lot.Id);
                _unitOfWork.Lot.Update(LotObj.Lot);

                var LotOwners = _unitOfWork.Lot_Owner.GetAll(o => o.LotId == LotObj.Lot.Id).ToList();
                if (LotObj.Owners == null)
                {
                    _unitOfWork.Lot_Owner.RemoveRange(LotOwners);
                }
                else
                {
                    // find new owners
                    var Current = false;
                    foreach (var selID in LotObj.Owners)
                    {
                        Current = false;
                        if (LotOwners.Where(o => o.OwnerId == selID).Count() > 0)
                        {
                            Current = true;
                        }
                        if(Current == false)
                        {
                            _unitOfWork.Lot_Owner.Add(new Lot_Owner() { OwnerId = selID, LotId = LotObj.Lot.Id });
                        }
                    }

                    // remove owners
                    var Remove = true;
                    foreach (var item in LotOwners)
                    {
                        Remove = true;
                        foreach (var selID in LotObj.Owners)
                        {
                            if(item.OwnerId == selID)
                            {
                                Remove = false;
                            }
                        }
                        if(Remove == true)
                        {
                            _unitOfWork.Lot_Owner.Remove(item);
                        }
                    }

                }

                // find removed inventory
                var LotInventory = _unitOfWork.Lot_Inventory.GetAll(o => o.LotId == LotObj.Lot.Id).ToList();
                if (LotObj.Inventory == null)
                {
                    _unitOfWork.Lot_Inventory.RemoveRange(LotInventory);
                }
                else
                {
                    // find new inventory
                    var Current = false;
                    foreach (var selID in LotObj.Inventory)
                    {
                        Current = false;
                        if (LotInventory.Where(o => o.InventoryId == selID).Count() > 0)
                        {
                            Current = true;
                        }
                        if (Current == false)
                        {
                            _unitOfWork.Lot_Inventory.Add(new Lot_Inventory() { InventoryId = selID, LotId = LotObj.Lot.Id });
                        }
                    }

                    // remove owners
                    var Remove = true;
                    foreach (var item in LotInventory)
                    {
                        Remove = true;
                        foreach (var selID in LotObj.Inventory)
                        {
                            if (item.InventoryId == selID)
                            {
                                Remove = false;
                            }
                        }
                        if (Remove == true)
                        {
                            _unitOfWork.Lot_Inventory.Remove(item);
                        }
                    }

                }

                _unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }
        #endregion
    }
}