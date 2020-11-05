using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Home.BoardMembers {
    public class Index : PageModel {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public Index(ApplicationDbContext db, IUnitOfWork unitOfWork) {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public List<Models.Owner> UserList { get; set; }
        
        public void OnGet() {
            UserList = _unitOfWork.Owner.GetAll().ToList();
            
        }
    }
}