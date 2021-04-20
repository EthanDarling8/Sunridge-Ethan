using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Lot.Files {
    [Authorize(Roles = SD.AdministratorRole)]
    public class Upsert : PageModel {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Upsert(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostEnvironment;
        }

        [BindProperty] public Models.LotFile LotFileObj { get; set; }
        public int LotId = 0;
        public DateTime date = DateTime.Now;
        public IActionResult OnGet(int? id) {
            if (!String.IsNullOrEmpty(Request.Query["lotid"]))
            {
                LotId = Int32.Parse(Request.Query["lotid"]);
            }
            LotFileObj = new Models.LotFile();
            if (id != null) {
                LotFileObj = _unitOfWork.LotFile.GetFirstOrDefault(f => f.Id == id);
                if (LotFileObj == null) {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost() {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;


            if (!ModelState.IsValid) {
                return Page();
            }

            if (LotFileObj.Id == 0) {
                // Upload and save image
                string fileName = Path.GetFileNameWithoutExtension(files[0].FileName) + "_" + Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"files\lot");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create)) {
                    files[0].CopyTo(fileStream);
                }

                LotFileObj.File = fileName + extension;
                LotFileObj.Date = date;
                _unitOfWork.LotFile.Add(LotFileObj);
            }
            else {
                // Update
                var objFromDb = _unitOfWork.LotFile.Get(LotFileObj.Id);
                objFromDb.Date = date;
                if (files.Count > 0) {
                    // Upload and save image
                    string fileName = Path.GetFileNameWithoutExtension(files[0].FileName) + "_" + Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"files\lot");
                    var extension = Path.GetExtension(files[0].FileName);

                    var filePath = Path.Combine(webRootPath, objFromDb.File.TrimStart('\\'));

                    if (System.IO.File.Exists(filePath)) {
                        System.IO.File.Delete(filePath);
                    }

                    using (var fileStream =
                        new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create)) {
                        files[0].CopyTo(fileStream);
                    }

                    LotFileObj.File = fileName + extension;
                }
                else {
                    LotFileObj.File = objFromDb.File;
                }

                _unitOfWork.LotFile.Update(LotFileObj);
            }

            _unitOfWork.Save();
            if (!String.IsNullOrEmpty(Request.Query["lotid"]))
            {
                LotId = Int32.Parse(Request.Query["lotid"]);
            return RedirectToPage("./index", new { lotid = LotId }); // If the LotId is valid return to files
            }
            else
            {
                return RedirectToPage("../index"); // If the Variable LotId is invalid return to lot
            }
        }
    }
}