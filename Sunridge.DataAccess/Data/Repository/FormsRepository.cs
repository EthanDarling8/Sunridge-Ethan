using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class FormsRepository : Repository<Forms>, IFormsRepository
    {
        private readonly ApplicationDbContext _db;

        public FormsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Forms updateObj)
        {
            var objFromDb = _db.Forms.FirstOrDefault(s => s.Id == updateObj.Id);
            objFromDb.DateModified = updateObj.DateModified;
            objFromDb.DateResolved = updateObj.DateResolved;
            objFromDb.Description = updateObj.Description;
            objFromDb.Suggestion = updateObj.Suggestion;
            objFromDb.Activity = updateObj.Activity;
            objFromDb.Hours = updateObj.Hours;
            objFromDb.Comments = updateObj.Comments;
            objFromDb.Resolved = updateObj.Resolved;
            objFromDb.AdminId = updateObj.AdminId;
            _db.SaveChanges();
        }
    }
}
