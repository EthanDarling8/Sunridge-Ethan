using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
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

        //Lists for displaying everything
        public IEnumerable<DocumentCategory> DocumentCategoryList { get; set; }
        public IEnumerable<DocumentSection> DocumentSectionList { get; set; }
        public IEnumerable<DocumentSectionText> DocumentSectionTextList { get; set; }

        //Search results storage
        public ICollection<DocumentSection> SearchedDocumentSectionList { get; set; }
        public ICollection<DocumentSectionText> SearchedDocumentSectionTextList { get; set; }

        [BindProperty]
        public string Search { get; set; }




        public void OnGet(int selectedCategoryId, string search)
        {
            SelectedCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == selectedCategoryId);
            Search = search;

            DocumentCategoryList = _unitOfWork.DocumentCategory.GetAll(null, c => c.OrderBy(c => c.Name));
            DocumentSectionList = _unitOfWork.DocumentSection.GetAll(null, s => s.OrderBy(s => s.DisplayOrder));
            DocumentSectionTextList = _unitOfWork.DocumentSectionText.GetAll(null, t => t.OrderBy(t => t.DisplayOrder));         

            if (Search != null)
            {
                //**** ToDo **** Searched File Keyword

                //Initialize
                SearchedDocumentSectionList = new List<DocumentSection>();
                SearchedDocumentSectionTextList = new List<DocumentSectionText>();

                //Add DocumentSection Name Results
                foreach (DocumentSection documentSection in DocumentSectionList.Where(s => s.Name.Contains(Search)))
                {
                    SearchedDocumentSectionList.Add(documentSection);
                }

                //Add DocumentSectionText Name Results
                foreach (DocumentSectionText documentSectionText in DocumentSectionTextList.Where(t => t.Name.Contains(Search)))
                {
                    SearchedDocumentSectionTextList.Add(documentSectionText);
                }

                //Add DocumentSectionText Text Results
                foreach (DocumentSectionText documentSectionText in DocumentSectionTextList.Where(t => t.Text.Contains(Search)))
                {
                    SearchedDocumentSectionTextList.Add(documentSectionText);
                }
            }
        }




        //Search        
        public IActionResult OnPostSearch()
        {
            return RedirectToPage("./Index", new { search = Search });
        }
    }
}
