using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Admin.PhotoCategory
{
    // **** ToDo **** [Authorize] only admins
    [Authorize]
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

                if (PhotoCategoryObj == null)
                {
                    //Returns a 404 error page.
                    return NotFound();
                }
            }

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
