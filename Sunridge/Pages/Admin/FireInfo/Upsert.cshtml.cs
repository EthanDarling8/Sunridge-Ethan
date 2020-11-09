using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System;
using System.IO;

namespace Sunridge.Pages.Admin.FireInfo
{
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
        public Models.FireInfo FireObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            FireObj = new Models.FireInfo();

            if (id != null)
            {
                FireObj = _unitOfWork.FireInfo.GetFirstOrDefault(u => u.Id == id);
                if (FireObj == null)
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

            if (FireObj.Id == 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\fireInfo");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                FireObj.Attachment = @"\images\fireInfo\" + fileName + extension;

                _unitOfWork.FireInfo.Add(FireObj);
                _unitOfWork.Save();
            }
            else
            {
                var objFromDb = _unitOfWork.FireInfo.Get(FireObj.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\fireInfo");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Attachment.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    FireObj.Attachment = @"\images\fireInfo\" + fileName + extension;
                }
                else
                {
                    FireObj.Attachment = objFromDb.Attachment;
                }

                _unitOfWork.FireInfo.Update(FireObj);
            }

            return RedirectToPage("./Index");
        }

    }
}
