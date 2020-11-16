using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUnitOfWork _unitOfWork;

        public Index(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [BindProperty] public string Search { get; set; }
        public List<Models.FireInfo> FireList { get; set; }

        public void OnGet() {
            FireList = _unitOfWork.FireInfo
                .GetAll(n => n.Archived == false && DateTime.Compare(n.DisplayDate.Date, DateTime.Now.Date) <= 0)
                .OrderBy(d => d.DisplayDate).Reverse().ToList();
        }
    }
}