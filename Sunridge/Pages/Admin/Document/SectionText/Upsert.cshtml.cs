using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;
using System.Collections.Generic;

namespace Sunridge.Pages.Admin.Document.SectionText
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
        public Models.DocumentSectionText DocumentSectionTextObj { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> SectionList { get; set; }

        public Models.DocumentSection DocumentSection { get; set; }



        // **** ToDO **** Setup passing in sectionId to have that section as the only option
        public IActionResult OnGet(int sectionTextId, int documentSectionId)
        {
            //Alwayas Initialize
            DocumentSectionTextObj = new Models.DocumentSectionText();

            SectionList = _unitOfWork.DocumentSection.GetListForDropDown();


            //Adding to a specified section from documents page, preserve section selection.
            DocumentSection = _unitOfWork.DocumentSection.GetFirstOrDefault(s => s.Id == documentSectionId);
            if(DocumentSection != null)
            {
                DocumentSectionTextObj.DocumentSectionId = DocumentSection.Id;
            }


            //Existing (edit)
            if (sectionTextId != 0)
            {
                //Get existing
                DocumentSectionTextObj = _unitOfWork.DocumentSectionText.GetFirstOrDefault(t => t.Id == sectionTextId);

                if (DocumentSectionTextObj == null)
                {
                    return RedirectToPage("/Home/Documents/Index");
                }                
            }

            return Page();
        }




        public IActionResult OnPost()
        {
            //New
            if (DocumentSectionTextObj.Id == 0)
            {                
                _unitOfWork.DocumentSectionText.Add(DocumentSectionTextObj);
            }
            //Existing
            else
            {
                _unitOfWork.DocumentSectionText.Update(DocumentSectionTextObj);
            }

            _unitOfWork.Save();
            
            return RedirectToPage("Index");
        }
    }
}