using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.Pages.Home.Classifieds.Details
{
    public class IndexModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ClassifiedsItemVM ClassifiedsItemObj { get; set; }

        public IEnumerable<ClassifiedsItem> ClassifiedsItemList { get; set; }

        public int ClassifiedsItemId { get; set; }
        
        public void OnGet(int id)
        {
            ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll().Where(x => x.Id == id);


        }
       

    }
}
