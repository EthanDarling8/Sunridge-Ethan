using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Owner.Forms
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Models.Forms> FormsResolvedList { get; set; }
        public List<Models.Forms> FormsUnResolvedList { get; set; }

        public void OnGet()
        {
            FormsResolvedList = _unitOfWork.Forms.GetAll(f => f.Resolved == true).ToList();
            FormsUnResolvedList = _unitOfWork.Forms.GetAll(f => f.Resolved == false).ToList();
        }
    }
}
