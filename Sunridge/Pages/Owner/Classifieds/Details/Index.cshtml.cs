using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Owner.Classifieds.Details
{
    public class DetailsModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ClassifiedsItemVM ClassifiedsItemObj { get; set; }

        //public IEnumerable<SelectListItem> ClassifiedsCategoryList { get; private set; }

        public IEnumerable<ClassifiedsItem> ClassifiedsItemList { get; set; }
        public IEnumerable<ClassifiedsCategory> ClassifiedsCategoryList { get; set; }

        public int ClassifiedsItemId { get; set; }
        public void OnGet(int id)
        {
            
            ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll().Where(x => x.Id == id);
            ClassifiedsCategoryList = _unitOfWork.ClassifiedsCategory.GetAll().ToList();
        }
    }
}
