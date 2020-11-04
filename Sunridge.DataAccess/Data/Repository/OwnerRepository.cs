using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    internal class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        private ApplicationDbContext _db;

        public OwnerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
