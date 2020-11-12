using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System;
using System.IO;

namespace Sunridge.Pages.Admin.News
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
        public Models.News NewsObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            NewsObj = new Models.News();

            if (id != null)
            {
                NewsObj = _unitOfWork.News.GetFirstOrDefault(u => u.Id == id);
                if (NewsObj == null)
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

            if (NewsObj.Id == 0)
            {
                if (files.Count > 0)
                {
                    string fileName = files[0].FileName;
                    var uploads = Path.Combine(webRootPath, @"images\News");
                    //var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    NewsObj.Attachment = @"\images\News\" + fileName;
                }

                _unitOfWork.News.Add(NewsObj);
                _unitOfWork.Save();
            }
            else
            {
                var objFromDb = _unitOfWork.News.Get(NewsObj.Id);

                if (files.Count > 0)
                {
                    string fileName = files[0].FileName;
                    var uploads = Path.Combine(webRootPath, @"images\News");

                    var imagePath = Path.Combine(webRootPath, objFromDb.Attachment.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    NewsObj.Attachment = @"\images\News\" + fileName;
                }
                else
                {
                    NewsObj.Attachment = objFromDb.Attachment;
                }

                _unitOfWork.News.Update(NewsObj);
            }

            return RedirectToPage("./Index");
        }

    }
}
