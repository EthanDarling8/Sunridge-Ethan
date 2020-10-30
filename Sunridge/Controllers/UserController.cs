using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data;
using Sunridge.Models;

namespace Sunridge.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db) {
            _db = db;
        }
        
        [Route("GetId")]
        [HttpGet]
        public async Task<string> GetId(string username) {
            string insID = "";
            IQueryable<ApplicationUser> usr = _db.Users.Where(u => u.Email == username);
            foreach (var u in usr) {
                insID = u.Id;
            }

            return insID;
        }

        [Route("GetImage")]
        [HttpGet]
        public async Task<string> GetImage(string username) {
            string profilePath = "";
            IQueryable<ApplicationUser> usr = _db.Users.Where(u => u.Email == username);
            foreach (var u in usr) {
                profilePath = u.Image;
            }
            return profilePath;
        }
    }
}