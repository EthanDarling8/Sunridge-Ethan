using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;

namespace Sunridge.Utility {
    public class FileUploadBase : PageModel {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public FileUploadBase() {
            // Empty constructor
        }
        
        public FileUploadBase(IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork, ApplicationDbContext db) {
            _hostingEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
            _db = db;
        }

        /// <summary>
        /// Uploads a file given a path string, and object representing a model, and a collection of files from a form.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        public async Task Upload(string path, object obj, IFormFileCollection formFiles) {
            var type = obj.GetType();

            foreach (var f in formFiles) {
                IFormFile file = f;
                string temp = file.FileName;
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, path);
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                await using (FileStream fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
                
                // OwnerBoardMemberVM Image Upload
                if (type == typeof(OwnerBoardMemberVM)) {
                    OwnerBoardMemberVM obvm = (OwnerBoardMemberVM) obj;
                    if (temp.Equals("")) {
                        obvm.BoardMember.Image = "/" + path + "/" + "DefaultImage.png";
                        await _db.SaveChangesAsync();
                    }
                    else {
                        obvm.BoardMember.Image = "/" + path + "/" + fileName;
                        await _db.SaveChangesAsync();
                    }
                    _unitOfWork.BoardMember.Update(obvm.BoardMember);
                    _unitOfWork.OwnerBoardMember.Update(obvm.OwnerBoardMember);
                }
            }
        }
    }
}