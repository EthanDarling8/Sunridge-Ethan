using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Home.BoardMembers {
    public class FileUploadBase : PageModel {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _db;


        public FileUploadBase() {
            // Empty constructor
        }
        
        public FileUploadBase(IWebHostEnvironment hostEnvironment, ApplicationDbContext db) {
            _hostingEnvironment = hostEnvironment;
            _db = db;
        }

        public async Task FileUpload(string path, object obj, IFormFileCollection formFiles) {
            var type = obj.GetType();
            
            OwnerBoardMemberVM obvm = new OwnerBoardMemberVM();
            if (type == typeof(OwnerBoardMemberVM)) {
                obvm = (OwnerBoardMemberVM) obj;
            }
            
            var fileName = "";
            var temp = "";
            foreach (var f in formFiles) {
                var file = f;
                temp = file.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, path);
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }

                if (temp.Equals("")) {
                    obvm.BoardMember.Image = "/" + path + "/DefaultImage.png";
                    await _db.SaveChangesAsync();
                }
                else {
                    obvm.BoardMember.Image = "/" + path + "/" + fileName;
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}