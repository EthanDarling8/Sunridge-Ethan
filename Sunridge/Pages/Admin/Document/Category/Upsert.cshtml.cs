using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Admin.Document.DocumentCategory
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
        public Models.DocumentCategory DocumentCategoryObj { get; set; }




        public IActionResult OnGet(int documentCategoryId)
        {
            DocumentCategoryObj = new Models.DocumentCategory();

            //Edit existing
            if (documentCategoryId != 0)
            {
                DocumentCategoryObj = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == documentCategoryId);

                if (DocumentCategoryObj == null)
                {
                    //Returns a 404 error page.
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

            //New PhotoCategory
            if (DocumentCategoryObj.Id == 0)
            {
                _unitOfWork.DocumentCategory.Add(DocumentCategoryObj);
            }
            //Existing PhotoCategory
            else
            {
                _unitOfWork.DocumentCategory.Update(DocumentCategoryObj);
            }

            _unitOfWork.Save();


            // **** ToDo **** Ensure redirects to desired location
            return RedirectToPage("./Index");
        }
    }
}