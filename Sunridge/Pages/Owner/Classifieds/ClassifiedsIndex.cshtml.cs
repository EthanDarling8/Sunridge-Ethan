using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Owner.Classifieds
{
    public class ClassifiedsIndexModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;

        public ClassifiedsIndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ClassifiedsItemVM ClassifiedsItemObj { get; set; }

        //public IEnumerable<SelectListItem> ClassifiedsCategoryList { get; private set; }

        public IEnumerable<ClassifiedsItem> ClassifiedsItemList { get; set; }
        //public IEnumerable<ClassifiedsCategory> ClassifiedsCategoryList { get; set; }
        public void OnGet()
        {
            ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll();
            //ClassifiedsCategoryList = _unitOfWork.ClassifiedsCategory.GetAll().ToList();
        }
    }
}
