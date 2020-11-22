using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;

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
        public IEnumerable<DocumentSectionText> DocumentSectionTextList { get; set; }

        //Search results storage
        public ICollection<DocumentFile> SearchedDocumentFileList { get; set; }
        public ICollection<DocumentSection> SearchedDocumentSectionList { get; set; }
        public ICollection<DocumentSectionText> SearchedDocumentSectionTextList { get; set; }
       



        public IActionResult OnGet(int selectedCategoryId, string search)
        {
            //Track selected category
            SelectedCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == selectedCategoryId);

            //Get all the categories, files, sections, and section text
            DocumentCategoryList = _unitOfWork.DocumentCategory.GetAll(null, c => c.OrderBy(c => c.Name));
            //Category Selected
            if (SelectedCategory != null)
            {                
                DocumentFileList = _unitOfWork.DocumentFile.GetAll(f => f.DocumentCategoryId == SelectedCategory.Id, f => f.OrderBy(f => f.DisplayOrder), "DocumentCategory");
                DocumentSectionList = _unitOfWork.DocumentSection.GetAll(s => s.DocumentCategoryId == SelectedCategory.Id, s => s.OrderBy(s => s.DisplayOrder), "DocumentCategory");
                DocumentSectionTextList = _unitOfWork.DocumentSectionText.GetAll(s => s.DocumentSection.DocumentCategoryId == SelectedCategory.Id, s => s.OrderBy(s => s.DisplayOrder), "DocumentSection");

                //Prevent manually entering empty Category ID in URL
                if (DocumentSectionList.Where(s => s.DocumentCategoryId == SelectedCategory.Id).Count() == 0 &&
                    DocumentFileList.Where(f => f.DocumentCategoryId == SelectedCategory.Id).Count() == 0 &&
                    !User.IsInRole(SD.AdministratorRole))
                {     
                    return RedirectToPage("Index");                    
                }
            }
            //No Category Selected
            else
            {
                DocumentFileList = _unitOfWork.DocumentFile.GetAll(null, s => s.OrderBy(s => s.DisplayOrder), "DocumentCategory");
                DocumentSectionList = _unitOfWork.DocumentSection.GetAll(null, s => s.OrderBy(s => s.DisplayOrder), "DocumentCategory");
                DocumentSectionTextList = _unitOfWork.DocumentSectionText.GetAll(null, s => s.OrderBy(s => s.DisplayOrder), "DocumentSection");
            }


            //Search
            if (search != null)
            {
                //Ignore case
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
                    if (SearchedDocumentSectionTextList.Where(t => t.Id == documentSectionText.Id).Count() == 0)
                    {
                        SearchedDocumentSectionTextList.Add(documentSectionText);
                    }
                }                
            }

            return Page();
        }




        //Search        
        public IActionResult OnPostSearch(int selectedCategoryId)
        {
            return RedirectToPage("Index", new { selectedCategoryId = selectedCategoryId, search = Search });
        }
    }
}
