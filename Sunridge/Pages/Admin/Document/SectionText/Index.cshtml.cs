using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.Document.SectionText
{
    public class IndexModel : PageModel
    {
        /* **** ToDo **** NONE OF THIS WORKS RIGHT NOW. THE Json IS NOT RENDERED BY THE DATATABLE
         * 
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [BindProperty]
        public DocumentSection DocumentSectionObj { get; set; }




        public IActionResult OnGet(int sectionId)
        {
            DocumentSectionObj = new DocumentSection();

            if (sectionId != 0)
            {
                DocumentSectionObj = _unitOfWork.DocumentSection.GetFirstOrDefault(s => s.Id == sectionId);

                if(DocumentSectionObj == null)
                {
                    return RedirectToPage("/Home/Documents/Index");
                }
            }

            return new JsonResult(_unitOfWork.DocumentSectionText.GetAll(t => t.DocumentSectionId == DocumentSectionObj.Id, null, "DocumentSection"));
        }
        */
    }
}
