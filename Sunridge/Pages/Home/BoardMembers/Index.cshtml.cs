using System;
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
        [BindProperty] public List<Lot_Owner> LotOwners { get; set; }
        [BindProperty] public OwnerBoardMemberVM OwnerBoardObj { get; set; }
        [BindProperty] public List<OwnerBoardMember> OwnerBoardMemberList { get; set; }

        public IActionResult OnGet(int? id) {
            OwnerBoardObj = new OwnerBoardMemberVM {
                OwnerBoardMember = new OwnerBoardMember(),
                Owner = new Models.Owner(),
                Lot = new Lot(),
                BoardMember = new BoardMember(),
                OwnerList = _unitOfWork.Owner.GetOwnerListForDropdown()
            };

            BoardMemberList = _unitOfWork.BoardMember.GetAll().ToList();
            OwnerList = _unitOfWork.Owner.GetAll().ToList();
            OwnerBoardMemberList = _unitOfWork.OwnerBoardMember.GetAll().ToList();
            LotOwners = _unitOfWork.Lot_Owner.GetAll().ToList();

            if (id != null) {
                OwnerBoardObj.BoardMember = _unitOfWork.BoardMember.GetFirstOrDefault(b => b.Id == id);
                if (OwnerBoardObj == null) {
                    return NotFound();
                }
            }

            foreach (var ob in OwnerBoardMemberList) {
                foreach (var lo in LotOwners) {
                    if (ob.OwnerId == lo.OwnerId) {
                        OwnerBoardObj.Lot = _unitOfWork.Lot.GetFirstOrDefault(l => l.Id == lo.LotId);
                    } 
                }
            }

            return Page();
        }
    }
}