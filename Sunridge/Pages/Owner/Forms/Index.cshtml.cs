using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Owner.Forms
{
    [Authorize(Roles = SD.AdministratorRole)]
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
            List<Models.Forms> Forms = _unitOfWork.Forms.GetAll(includeProperties:"Owner").ToList();

            FormsResolvedList = Forms.FindAll(f => f.Resolved == true);
            FormsUnResolvedList = Forms.FindAll(f => f.Resolved == false);
        }
    }
}
