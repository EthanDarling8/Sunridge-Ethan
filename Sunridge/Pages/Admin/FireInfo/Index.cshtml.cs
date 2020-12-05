using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.FireInfo
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Models.FireInfo> FireList { get; set; }

        public void OnGet()
        {
            FireList = _unitOfWork.FireInfo.GetAll().ToList();
        }
    }
}
