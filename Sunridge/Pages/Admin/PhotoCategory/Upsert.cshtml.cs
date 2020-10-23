using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Admin.PhotoCategory
{
    // **** ToDo **** [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [BindProperty]
        public Models.PhotoCategory PhotoCategoryObj { get; set; }




        public IActionResult OnGet(int? PhotoCategoryId)
        {
            PhotoCategoryObj = new Models.PhotoCategory();

            //Edit existing
            if (PhotoCategoryId != null)
            {
                PhotoCategoryObj = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == PhotoCategoryId);

                if (PhotoCategoryId == null)
                {
                    //Returns a 404 error page.
                    return NotFound();
                }
            }

            //This refreshes the page which triggers the OnGet again. ID will be null.
            return Page();

        }


        

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            
            //New PhotoCategory
            if (PhotoCategoryObj.Id == 0)
            {
                _unitOfWork.PhotoCategory.Add(PhotoCategoryObj);
            }
            //Existing PhotoCategory
            else
            {
                _unitOfWork.PhotoCategory.Update(PhotoCategoryObj);
            }

            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}
