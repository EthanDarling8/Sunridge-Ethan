using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Home.Documents
{
    public class IndexModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        public DocumentCategory SelectedCategory { get; set; }

        public IEnumerable<DocumentCategory> DocumentCategoryList { get; set; }
        public IEnumerable<DocumentSection> DocumentSectionList { get; set; }
        public IEnumerable<DocumentSectionText> DocumentSectionTextList { get; set; }

        [BindProperty]
        public string Search { get; set; }




        public void OnGet(int selectedCategoryId, string search)
        {
            SelectedCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == selectedCategoryId);
            Search = search;

            DocumentCategoryList = _unitOfWork.DocumentCategory.GetAll(null, c => c.OrderBy(c => c.Name));

            if(SelectedCategory != null)
            {
                DocumentSectionList = _unitOfWork.DocumentSection.GetAll(s => s.DocumentCategoryId == SelectedCategory.Id, s => s.OrderBy(s => s.DisplayOrder));
                DocumentSectionTextList = _unitOfWork.DocumentSectionText.GetAll(null, t => t.OrderBy(t => t.DisplayOrder));
            }            

            if (Search != null)
            {
                // **** ToDo **** Search file tiles, keywords, section titles, and text *@
            }
        }




        //Search        
        public IActionResult OnPostSearch()
        {
            return RedirectToPage("./Index", new { search = Search });
        }
    }
}
