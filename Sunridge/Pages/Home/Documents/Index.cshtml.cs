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

        [BindProperty]
        public string Search { get; set; }




        public void OnGet(int selectedCategoryId, string search)
        {
            SelectedCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == selectedCategoryId);
            Search = search;

            DocumentCategoryList = _unitOfWork.DocumentCategory.GetAll(null, c => c.OrderBy(c => c.Name));

            if (Search != null)
            {
                // **** ToDo **** Search file tiles, keywords, section titles, and text *@
            }
        }




        //Search        
        public IActionResult OnPostSearch()
        {
            return RedirectToPage("./Index", new { selectedCategoryId = SelectedCategory.Id, search = Search });
        }
    }
}
