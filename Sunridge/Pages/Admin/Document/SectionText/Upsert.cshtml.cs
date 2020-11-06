using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System.Collections.Generic;

namespace Sunridge.Pages.Admin.Document.SectionText
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
        public Models.DocumentSectionText DocumentSectionTextObj { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> SectionList { get; set; }




        public IActionResult OnGet(int sectionTextId)
        {
            //Alwayas Initialize
            DocumentSectionTextObj = new Models.DocumentSectionText();
            SectionList = _unitOfWork.DocumentSection.GetListForDropDown();
            

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