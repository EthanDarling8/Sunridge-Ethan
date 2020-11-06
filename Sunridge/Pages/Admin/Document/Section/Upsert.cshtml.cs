using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System.Collections.Generic;

namespace Sunridge.Pages.Admin.Document.Section
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
        public Models.DocumentSection DocumentSectionObj { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> CategoryList { get; set; }




        public IActionResult OnGet(int sectionId)
        {
            //Alwayas Initialize
            DocumentSectionObj = new Models.DocumentSection();
            CategoryList = _unitOfWork.DocumentCategory.GetListForDropDown();
            

            //Existing (edit)
            if (sectionId != 0)
            {
                //Get existing
                DocumentSectionObj = _unitOfWork.DocumentSection.GetFirstOrDefault(s => s.Id == sectionId);

                if (DocumentSectionObj == null)
                {
                    return RedirectToPage("/Home/Documents/Index");
                }                
            }

            return Page();
        }




        public IActionResult OnPost()
        {
            //New
            if (DocumentSectionObj.Id == 0)
            {                
                _unitOfWork.DocumentSection.Add(DocumentSectionObj);
            }
            //Existing
            else
            {
                _unitOfWork.DocumentSection.Update(DocumentSectionObj);
            }

            _unitOfWork.Save();
            
            return RedirectToPage("Index");
        }
    }
}