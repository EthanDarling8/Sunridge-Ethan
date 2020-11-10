using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using Sunridge.Pages.Admin.News;
using Sunridge.Utility;

namespace Sunridge.Pages.Home.BoardMembers {
    public class UpsertModel : FileUploadBase {
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

            if (id != null) {
                OwnerBoardObj.BoardMember = _unitOfWork.BoardMember.GetFirstOrDefault(b => b.Id == id);
                if (OwnerBoardObj == null) {
                    return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            IFormFileCollection files = HttpContext.Request.Form.Files;

            OwnerBoardObj.OwnerBoardMember.Owner =
                await _userManager.FindByIdAsync(OwnerBoardObj.OwnerBoardMember.Owner.Id);
            _unitOfWork.BoardMember.Add(OwnerBoardObj.BoardMember);
            _unitOfWork.Save();

            OwnerBoardObj.OwnerBoardMember.BoardMemberId = OwnerBoardObj.BoardMember.Id;
            _unitOfWork.OwnerBoardMember.Add(OwnerBoardObj.OwnerBoardMember);
            _unitOfWork.Save();

            FileUploadBase fileUploader = new FileUploadBase(_hostingEnvironment, _unitOfWork, _db);

            await fileUploader.Upload(@"/images/boardMembers/", OwnerBoardObj, files);
            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}