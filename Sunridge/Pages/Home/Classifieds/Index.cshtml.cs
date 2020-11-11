using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Home.Classifieds
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

        //public IEnumerable<SelectListItem> ClassifiedsCategoryList { get; private set; }

        public IEnumerable<ClassifiedsItem> ClassifiedsItemList { get; set; }
        public IEnumerable<ClassifiedsCategory> ClassifiedsCategoryList { get; set; }
        public IEnumerable<ClassifiedsSubcategory> ClassifiedsSubcategoryList { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        
        public void OnGet(string catid, string subcatid)
        {
            if (catid != null)
            {
                CategoryId = int.Parse(catid);
                ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll().Where(x => x.CategoryId == CategoryId);

                //if (subcatid != null)
                //{
                //    SubcategoryId = int.Parse(subcatid);
                //    ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll().Where
                //        (x => x.CategoryId == CategoryId && x.SubcategoryId == SubcategoryId);
                //}
            }
            else
            {
                ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll();
            }

            ClassifiedsCategoryList = _unitOfWork.ClassifiedsCategory.GetAll();
            ClassifiedsSubcategoryList = _unitOfWork.ClassifiedsSubcategory.GetAll();
        }
    }
}

