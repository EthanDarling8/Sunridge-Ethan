using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Home.FireInfo {
    public class Upsert : PageModel {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Upsert(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostEnvironment;
        }

        [BindProperty] public Models.FireInfo FireInfoObj { get; set; }

        public IActionResult OnGet(int? id) {
            FireInfoObj = new Models.FireInfo();

            if (id != null) {
                FireInfoObj = _unitOfWork.FireInfo.GetFirstOrDefault(f => f.Id == id);
                if (FireInfoObj == null) {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost() {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            FireInfoObj.PostDate = DateTime.Today.ToString("d");

            if (!ModelState.IsValid) {
                return Page();
            }

            if (FireInfoObj.Id == 0) {
                // Upload and save image
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"fireinfo");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create)) {
                    files[0].CopyTo(fileStream);
                }

                FireInfoObj.File = fileName + extension;

                _unitOfWork.FireInfo.Add(FireInfoObj);
            }
            else {
                // Update
                var objFromDb = _unitOfWork.FireInfo.Get(FireInfoObj.Id);
                if (files.Count > 0) {
                    // Upload and save image
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath,@"fireinfo");
                    var extension = Path.GetExtension(files[0].FileName);

                    var filePath = Path.Combine(webRootPath, objFromDb.File.TrimStart('\\'));

                    if (System.IO.File.Exists(filePath)) {
                        System.IO.File.Delete(filePath);
                    }

                    using (var fileStream =
                        new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create)) {
                        files[0].CopyTo(fileStream);
                    }

                    FireInfoObj.File = fileName + extension;
                }
                else {
                    FireInfoObj.File = objFromDb.File;
                }

                _unitOfWork.FireInfo.Update(FireInfoObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}