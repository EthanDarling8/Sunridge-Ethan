using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Home.BoardMembers {
    public class UpsertModel : PageModel {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Models.Owner> _userManager;

        public UpsertModel(IUnitOfWork unitOfWork,
            UserManager<Models.Owner> userManager,
            IWebHostEnvironment hostEnvironment,
            ApplicationDbContext db) {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _hostingEnvironment = hostEnvironment;
            _db = db;
        }

        [BindProperty] public OwnerBoardMemberVM OwnerBoardObj { get; set; }

        public IActionResult OnGet(int? id) {
            OwnerBoardObj = new OwnerBoardMemberVM {
                BoardMember = new BoardMember(),
                OwnerList = _unitOfWork.Owner.GetOwnerListForDropdown()
            };

            if (id != null) {
                OwnerBoardObj.BoardMember =
                    _unitOfWork.BoardMember.GetFirstOrDefault(b => b.Id == id);

                if (OwnerBoardObj.BoardMember == null) {
                    return NotFound();
                }
            }
            else {
                OwnerBoardObj.BoardMember.OwnerId = id.ToString();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(int id) {
            if (!ModelState.IsValid) {
                return Page();
            }

            if (OwnerBoardObj.BoardMember.Id == 0) {
                var fileName = "";
                var temp = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files) {
                    var file = Image;
                    temp = file.FileName;
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"images\boardMembers");
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create)) {
                        await file.CopyToAsync(fileStream);
                    }

                    if (temp.Equals("")) {
                        OwnerBoardObj.BoardMember.Image = "/images/boardMembers/DefaultImage.png";
                        await _db.SaveChangesAsync();
                    }
                    else {
                        OwnerBoardObj.BoardMember.Image = fileName;
                        _unitOfWork.BoardMember.Add(OwnerBoardObj.BoardMember);
                    }
                }

                if (OwnerBoardObj.BoardMember.Id == 0) {
                    OwnerBoardObj.BoardMember.Owner =
                        _unitOfWork.Owner.GetFirstOrDefault(u => u.Id == id.ToString());
                    _unitOfWork.BoardMember.Add(OwnerBoardObj.BoardMember);
                }
                else {
                    OwnerBoardObj.BoardMember.Owner =
                        _unitOfWork.Owner.GetFirstOrDefault(u => u.Id == OwnerBoardObj.BoardMember.OwnerId);
                    _unitOfWork.BoardMember.Update(OwnerBoardObj.BoardMember);
                }
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}