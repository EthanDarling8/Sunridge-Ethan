using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Home.News
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public string Search { get; set; }
        public List<Models.News> NewsList { get; set; }

        public void OnGet()
        {
            NewsList = _unitOfWork.News.GetAll(n => n.Archived == false && DateTime.Compare(n.DisplayDate.Date, DateTime.Now.Date) <= 0).OrderBy(d => d.DisplayDate).Reverse().ToList();
            int i = 0;
            foreach(var item in NewsList)
            {
                if(item.Attachment != null)
                {
                    NewsList[i].DisplayName = Path.GetFileName(item.Attachment);
                }
                i++;
            }
        }
    }
}
