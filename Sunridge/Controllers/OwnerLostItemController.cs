using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerLostItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public OwnerLostItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public string OwnerId { get; set; }
        [HttpGet]
        public IActionResult Get()
        {
            // Set Expired Listings to Inactive
            var expiredListings = _context.LostItem.Where(li => li.ExpirationDate < DateTime.Now).ToList();
            expiredListings.ForEach(li => li.Active = false);
            _context.SaveChanges();
            // Return Lost Items
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
            {
                OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return Json(new { data = _unitOfWork.LostItem.GetAll().Where(d => d.OwnerId == OwnerId && d.Active ==true) });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.LostItem.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                //physically remove image if it exists
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.Trim('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _unitOfWork.LostItem.Remove(objFromDb);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = true, message = "Delete succcessful" });
        }

        [HttpPost("{id}")]
        public IActionResult Resolve(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.LostItem.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while resolving" });
                }


                objFromDb.Active = false;
                _unitOfWork.LostItem.Update(objFromDb);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while resolving" });
            }
            return Json(new { success = true, message = "Listing resolved" });
        }
    }

}
