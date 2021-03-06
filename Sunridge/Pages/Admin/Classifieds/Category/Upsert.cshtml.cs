using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Classifieds.Category
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.ClassifiedsCategory ClassifiedsCategoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            ClassifiedsCategoryObj = new Models.ClassifiedsCategory();

            if (id != null)
            {
                ClassifiedsCategoryObj = _unitOfWork.ClassifiedsCategory.GetFirstOrDefault(u => u.Id == id);

                if (ClassifiedsCategoryObj == null)
                {
                    return NotFound();
                }
            }

            return Page(); //refresh page, call onGet again without id

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ClassifiedsCategoryObj.Id == 0) //means a brand new category
            {
                _unitOfWork.ClassifiedsCategory.Add(ClassifiedsCategoryObj);
            }

            else
            {
                _unitOfWork.ClassifiedsCategory.Update(ClassifiedsCategoryObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }



    }
}