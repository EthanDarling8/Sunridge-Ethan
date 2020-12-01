using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeLostItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public HomeLostItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Set Expired Listings to Inactive
            var expiredListings = _context.LostItem.Where(li => li.ExpirationDate < DateTime.Now).ToList();
            expiredListings.ForEach(li => li.Active = false);
            _context.SaveChanges();
            // Return Lost Items
            return Json(new { data = _unitOfWork.LostItem.GetAll(li => li.Active == true) }); // If the User is an Owner or General Public they only see active listings.
        }
    }

}
