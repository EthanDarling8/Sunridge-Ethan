using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Home.FireInfo {
    public class Index : PageModel {
        /*private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Index(
            ApplicationDbContext db
            , IUnitOfWork unitOfWork
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , IWebHostEnvironment webHostEnvironment) {
            _db = db;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty] public IFormFile Upload{ get; set; }

        public void OnGet(int? id) {
        }*/
    }
}