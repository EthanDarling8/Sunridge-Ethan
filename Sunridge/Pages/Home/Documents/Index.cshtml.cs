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


        [BindProperty]
        public DocumentCategory SelectedCategory { get; set; }

        [BindProperty]
        public string Search { get; set; }

        //Lists for displaying everything
        public IEnumerable<DocumentCategory> DocumentCategoryList { get; set; }
        public IEnumerable<DocumentSection> DocumentSectionList { get; set; }
        public IEnumerable<DocumentFile> DocumentFileList { get; set; }
        public ICollection<DocumentSectionText> DocumentSectionTextList { get; set; }

        //Search results storage
        public ICollection<DocumentFile> SearchedDocumentFileList { get; set; }
        public ICollection<DocumentSection> SearchedDocumentSectionList { get; set; }
        public ICollection<DocumentSectionText> SearchedDocumentSectionTextList { get; set; }
       



        public void OnGet(int selectedCategoryId, string search)
        {
            DocumentCategoryList = _unitOfWork.DocumentCategory.GetAll(null, c => c.OrderBy(c => c.Name));
            SelectedCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == selectedCategoryId);           


            if (SelectedCategory != null)
            {
                DocumentFileList = _unitOfWork.DocumentFile.GetAll(s => s.DocumentCategoryId == SelectedCategory.Id, s => s.OrderBy(s => s.DisplayOrder));
                DocumentSectionList = _unitOfWork.DocumentSection.GetAll(s => s.DocumentCategoryId == SelectedCategory.Id, s => s.OrderBy(s => s.DisplayOrder));
                //Must initialize
                DocumentSectionTextList = new List<DocumentSectionText>();

                //Add DocumentSectionText
                foreach (DocumentSection documentSection in DocumentSectionList)
                {
                    IEnumerable<DocumentSectionText> tempText = new List<DocumentSectionText>();
                    tempText = _unitOfWork.DocumentSectionText.GetAll(t => t.DocumentSectionId == documentSection.Id, t => t.OrderBy(t => t.DisplayOrder));

                    foreach (DocumentSectionText documentSectionText in tempText)
                    {
                        DocumentSectionTextList.Add(documentSectionText);
                    }
                }
            }


            if (search != null)
            {
                Search = search.ToLower();
                

                //Initialize
                SearchedDocumentFileList = new List<DocumentFile>();
                SearchedDocumentSectionList = new List<DocumentSection>();
                SearchedDocumentSectionTextList = new List<DocumentSectionText>();

                //Add DocumentFile Name Results
                foreach (DocumentFile documentFile in DocumentFileList.Where(s => s.Name.ToLower().Contains(Search)))
                {
                    SearchedDocumentFileList.Add(documentFile);
                }

                //**** ToDo **** Searched File Keyword results

                //Add DocumentSection Name Results
                foreach (DocumentSection documentSection in DocumentSectionList.Where(s => s.Name.ToLower().Contains(Search)))
                {
                    SearchedDocumentSectionList.Add(documentSection);
                }

                //Add DocumentSectionText Name Results
                foreach (DocumentSectionText documentSectionText in DocumentSectionTextList.Where(t => t.Name.ToLower().Contains(Search)))
                {
                    SearchedDocumentSectionTextList.Add(documentSectionText);
                }

                //Add DocumentSectionText Text Results
                foreach (DocumentSectionText documentSectionText in DocumentSectionTextList.Where(t => t.Text.ToLower().Contains(Search)))
                {
                    //Only add the section if it wasn't already added
                    if(SearchedDocumentSectionTextList.Where(t => t.Id == documentSectionText.Id).Count() == 0)
                    {
                        SearchedDocumentSectionTextList.Add(documentSectionText);
                    }                    
                }
            }
        }




        //Search        
        public IActionResult OnPostSearch(int selectedCategoryId)
        {
            return RedirectToPage("./Index", new { selectedCategoryId = selectedCategoryId, search = Search });
        }
    }
}
