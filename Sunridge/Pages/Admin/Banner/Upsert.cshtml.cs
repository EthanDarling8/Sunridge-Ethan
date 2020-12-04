using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Banner
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Models.Banner BannerObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            BannerObj = new Models.Banner();

            if (id != null)
            {
                BannerObj = _unitOfWork.Banner.GetFirstOrDefault(u => u.Id == id);
                if (BannerObj == null)
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (BannerObj.Id == 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\BannerImages");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                BannerObj.Image = @"\images\BannerImages\" + fileName + extension;

                _unitOfWork.Banner.Add(BannerObj);
                _unitOfWork.Save();
            }
            else
            {
                var objFromDb = _unitOfWork.Banner.Get(BannerObj.Id);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\BannerImages");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    BannerObj.Image = @"\images\BannerImages\" + fileName + extension;
                }
                else
                {
                    BannerObj.Image = objFromDb.Image;
                }

                _unitOfWork.Banner.Update(BannerObj);
            }

            return RedirectToPage("./Index");
        }

    }
}
