using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.CommonAreaAssets.MaintenanceRecords
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            MaintenanceRecord = new MaintenanceRecord();

            if (id != null)
            {
                MaintenanceRecord = _unitOfWork.MaintenanceRecord.GetFirstOrDefault(mr => mr.Id == id);

                if (MaintenanceRecord == null)
                {
                    return NotFound();
                }

            }

            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (MaintenanceRecord.Id == 0) // check if brand new MaintenanceRecord
            {
                _unitOfWork.MaintenanceRecord.Add(MaintenanceRecord);
            }
            else
            {
                _unitOfWork.MaintenanceRecord.Update(MaintenanceRecord);
            }

            _unitOfWork.Save();

            return Redirect("Index?id=" + MaintenanceRecord.AssetId);
        }
    }
}