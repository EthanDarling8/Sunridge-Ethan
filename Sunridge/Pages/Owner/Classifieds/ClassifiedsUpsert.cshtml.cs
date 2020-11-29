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
using Microsoft.AspNetCore.Authorization;

namespace Sunridge.Pages.Owner.Classifieds
{
    [Authorize]
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
        public IEnumerable<SelectListItem> OwnersList { get; private set; }
        //public IEnumerable<SelectListItem> ClassifiedsSubcategoryList { get; private set; }
        public IActionResult OnGet(int? id)
        {
            

            ClassifiedsItemObj = new ClassifiedsItemVM
                {
                    ClassifiedsItem = new Models.ClassifiedsItem(),
                    ClassifiedsCategoryList = _unitOfWork.ClassifiedsCategory.GetClassifiedsCategoryListForDropDown(),
                    //ClassifiedsSubcategoryList = _unitOfWork.ClassifiedsSubcategory.GetClassifiedsSubcategoryListForDropDown()
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

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;



            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            ClassifiedsItemObj.ClassifiedsItem.OwnerId = User.Claims.ToList()[0].Value;

            if (ClassifiedsItemObj.ClassifiedsItem.Id == 0) //means a brand new menu item
            {
                    //Physically upload and save image
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"img\classifiedsitems");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    //save the string data path
                    ClassifiedsItemObj.ClassifiedsItem.Images = @"\img\classifiedsitems\" + fileName + extension;


                    _unitOfWork.ClassifiedsItem.Add(ClassifiedsItemObj.ClassifiedsItem);
                
            }

            else //update
            {
                var objFromDb = _unitOfWork.ClassifiedsItem.Get(ClassifiedsItemObj.ClassifiedsItem.Id);
                if (files.Count > 0)
                {
                    
                        //Physically upload and save image
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"img\classifiedsitems");
                        var extension = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, objFromDb.Images.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        //save the string data path
                        ClassifiedsItemObj.ClassifiedsItem.Images = @"\img\classifiedsitems\" + fileName + extension;
                    
                }
                else
                {
                    ClassifiedsItemObj.ClassifiedsItem.Images = objFromDb.Images;
                }
                _unitOfWork.ClassifiedsItem.Update(ClassifiedsItemObj.ClassifiedsItem);
            }

            _unitOfWork.Save();
            return RedirectToPage("./ClassifiedsIndex");
        }



    }
}
