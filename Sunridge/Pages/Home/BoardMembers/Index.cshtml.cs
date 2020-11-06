using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Home.BoardMembers {
    public class Index : PageModel {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public Index(ApplicationDbContext db, IUnitOfWork unitOfWork) {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        [BindProperty] public List<BoardMember> BoardMemberList { get; set; }
        [BindProperty] public List<Models.Owner> OwnerList { get; set; }
        [BindProperty] public OwnerBoardMemberVM BoardMemberObj { get; set; }

        public IActionResult OnGet(int? id) {
            BoardMemberObj = new OwnerBoardMemberVM {
                BoardMember = new BoardMember(),
                OwnerList = _unitOfWork.Owner.GetOwnerListForDropdown()
            };

            BoardMemberList = _unitOfWork.BoardMember.GetAll().ToList();
            OwnerList = _unitOfWork.Owner.GetAll().ToList();

            if (id != null) {
                BoardMemberObj.BoardMember = _unitOfWork.BoardMember.GetFirstOrDefault(b => b.Id == id);
                if (BoardMemberObj == null) {
                    return NotFound();
                }
            }

            return Page();
        }
    }
}