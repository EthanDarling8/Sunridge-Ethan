using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    internal class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
