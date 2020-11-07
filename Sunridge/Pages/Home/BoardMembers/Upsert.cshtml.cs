using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using Sunridge.Pages.Admin.News;

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
                OwnerBoardMember = new OwnerBoardMember(),
                Owner = new Models.Owner(),
                BoardMember = new BoardMember(),
                OwnerList = _unitOfWork.Owner.GetOwnerListForDropdown()
            };

            return Page();
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var fileName = "";
            var temp = "";
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files) {
                var file = Image;
                temp = file.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"images\boardMembers");
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }

                if (temp.Equals("")) {
                    OwnerBoardObj.BoardMember.Image = "/images/boardMembers/DefaultImage.png";
                    await _db.SaveChangesAsync();
                }
                else {
                    OwnerBoardObj.BoardMember.Image = "/images/boardMembers/" + fileName;
                }
            }

            OwnerBoardObj.OwnerBoardMember.Owner = await _userManager.FindByIdAsync(OwnerBoardObj.OwnerBoardMember.Owner.Id);
            _unitOfWork.BoardMember.Add(OwnerBoardObj.BoardMember);
            _unitOfWork.Save();

            OwnerBoardObj.OwnerBoardMember.BoardMemberId = OwnerBoardObj.BoardMember.Id;
            _unitOfWork.OwnerBoardMember.Add(OwnerBoardObj.OwnerBoardMember);
            _unitOfWork.Save();
            
            return RedirectToPage("./Index");
        }
    }
}