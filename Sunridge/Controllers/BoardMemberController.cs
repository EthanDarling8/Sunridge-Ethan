using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BoardMemberController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public BoardMemberController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.BoardMember.GetFirstOrDefault(unitOfWork => unitOfWork.Id == id);
            _unitOfWork.BoardMember.Remove(objFromDb);
            _unitOfWork.Save();
            return RedirectToPage("/Admin/BoardMembers/Index");
        }
    }
}