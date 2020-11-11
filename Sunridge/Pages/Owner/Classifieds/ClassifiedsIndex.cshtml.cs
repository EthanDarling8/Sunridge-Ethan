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

namespace Sunridge.Pages.Owner.Classifieds
{
    [Authorize]
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
        public IEnumerable<ClassifiedsCategory> ClassifiedsCategoryList { get; set; }
        //public IEnumerable<ClassifiedsSubcategory> ClassifiedsSubcategoryList { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }

        public void OnGet(string catid, string subcatid)
        {
            //if (ownerid != null)
            //{
            //    ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll().Where(x => x.OwnerId == ownerid);
            //}
            //else
            //{
                ClassifiedsItemList = _unitOfWork.ClassifiedsItem.GetAll();
            //}

            if (catid != null)
            {
                CategoryId = int.Parse(catid);
                ClassifiedsItemList = ClassifiedsItemList.Where(x => x.CategoryId == CategoryId);

                //if (subcatid != null)
                //{
                //    SubcategoryId = int.Parse(subcatid);
                //    ClassifiedsItemList = ClassifiedsItemList.Where(x => x.Category != null && x.SubcategoryId == SubcategoryId);
                //    //ClassifiedsSubcategoryList = _unitOfWork.ClassifiedsSubcategory.GetAll().Where(x => x.CategoryId == CategoryId);
                //}
            }
            //else
            //{
            //    ClassifiedsSubcategoryList = _unitOfWork.ClassifiedsSubcategory.GetAll();
            //}

            ClassifiedsCategoryList = _unitOfWork.ClassifiedsCategory.GetAll();
        }
    }
}
