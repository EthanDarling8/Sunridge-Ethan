using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Document.Section
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }


        /* **** ToDo **** Clean this up if you don't need it
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }





        public IActionResult OnGet(int categoryId)
        {
            if (categoryId != 0)
            {
                DocumentCategoryObj = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == categoryId);

                if (DocumentCategoryObj == null)
                {
                    return RedirectToPage("/Home/Documents/Index");
                }

                return Page();  

                //**** ToDo **** Pass categoryId to controller

                //return RedirectToPage("/api/documentCategory/" + categoryId);
                //return Redirect("/Controllers/DocumentSectionController/" + categoryId);
            }

            // No category specified: invalid
            else
            {
                return RedirectToPage("/Home/Documents/Index");
            }
        }
        */
    }
}
