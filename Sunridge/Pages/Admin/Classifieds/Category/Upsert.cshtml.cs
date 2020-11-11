using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Admin.Classifieds.Category
{
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
