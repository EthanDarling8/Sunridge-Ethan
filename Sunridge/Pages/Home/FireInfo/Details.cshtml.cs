﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Home.FireInfo {
    public class Details : PageModel {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Details(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostEnvironment;
        }
        
        [BindProperty] public Models.FireInfo FireInfoObj { get; set; }
        
        public IActionResult OnGet(int? id) {
            FireInfoObj = new Models.FireInfo();

            if (id != null) {
                FireInfoObj = _unitOfWork.FireInfo.GetFirstOrDefault(f => f.Id == id);
                if (FireInfoObj == null) {
                    return NotFound();
                }
            }

            return Page();
        }
    }
}