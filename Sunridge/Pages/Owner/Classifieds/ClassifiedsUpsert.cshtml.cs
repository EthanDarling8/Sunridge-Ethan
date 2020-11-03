using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Pages.Admin.Banner;


namespace Sunridge.Pages.Owner.Classifieds
{
    public class ClassifiedsUpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ClassifiedsUpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public ClassifiedsItemVM ClassifiedsItemObj { get; set; }

        public IEnumerable<SelectListItem> ClassifiedsCategoryList { get; private set; }

        public IActionResult OnGet(int? id)
        {
            ClassifiedsItemObj = new ClassifiedsItemVM
            {
                ClassifiedsItem = new Models.ClassifiedsItem(),
                ClassifiedsCategoryList = _unitOfWork.ClassifiedsCategory.GetClassifiedsCategoryListForDropDown(),
                ClassifiedsSubcategoryList = _unitOfWork.ClassifiedsSubcategory.GetClassifiedsSubcategoryListForDropDown()
            };
        if (id != null)
            {
                ClassifiedsItemObj.ClassifiedsItem = _unitOfWork.ClassifiedsItem.GetFirstOrDefault(u => u.Id == id);

                if (ClassifiedsItemObj.ClassifiedsItem == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }
        
        //public IActionResult OnPost()
        //{
           
        //}
    }
}
