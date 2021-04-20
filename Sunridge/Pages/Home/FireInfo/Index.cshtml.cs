using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

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
            int i = 0;
            foreach (var item in FireList) {
                if (item.Attachment != null) {
                    FireList[i].DisplayName = Path.GetFileName(item.Attachment);
                }
                i++;
            }
        }
    }
}