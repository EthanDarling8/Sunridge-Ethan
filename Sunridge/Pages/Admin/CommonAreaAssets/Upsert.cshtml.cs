using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.CommonAreaAssets
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Asset Asset { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            Asset = new Asset();

            if (id != null)
            {
                Asset = _unitOfWork.Asset.GetFirstOrDefault(a => a.Id == id);

                if (Asset == null)
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

            if (Asset.Id == 0) // check if brand new Asset
            {
                _unitOfWork.Asset.Add(Asset);
            }
            else
            {
                _unitOfWork.Asset.Update(Asset);
            }

            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }

    }
}