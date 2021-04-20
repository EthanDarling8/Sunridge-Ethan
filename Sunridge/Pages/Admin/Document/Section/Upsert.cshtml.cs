using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;
using System.Collections.Generic;

namespace Sunridge.Pages.Admin.Document.Section
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
        public Models.DocumentSection DocumentSectionObj { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        
        

        public IActionResult OnGet(int sectionId, int documentCategoryId)
        {
            //Alwayas Initialize
            DocumentSectionObj = new Models.DocumentSection();
            CategoryList = _unitOfWork.DocumentCategory.GetListForDropDown();


            //Adding new from documents page (selected category preserved)
            if (documentCategoryId != 0)
            {
                //Get existing
                DocumentSectionObj.DocumentCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == documentCategoryId);

                if (DocumentSectionObj.DocumentCategory == null)
                {
                    return RedirectToPage("/Home/Documents/Index");
                }
                else
                {
                    DocumentSectionObj.DocumentCategoryId = DocumentSectionObj.DocumentCategory.Id;
                }
            }
            //Existing (edit)
            else if (sectionId != 0)
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