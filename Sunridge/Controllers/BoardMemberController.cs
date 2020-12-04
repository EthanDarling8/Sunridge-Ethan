using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.AdministratorRole)]
    public class BoardMemberController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public BoardMemberController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        
        [Authorize(Roles = SD.AdministratorRole)]
        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.BoardMember.GetFirstOrDefault(unitOfWork => unitOfWork.Id == id);
            _unitOfWork.BoardMember.Remove(objFromDb);
            _unitOfWork.Save();
            return RedirectToPage("/Admin/BoardMembers/Index");
        }
    }
}