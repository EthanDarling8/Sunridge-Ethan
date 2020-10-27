using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        private readonly ApplicationDbContext _db;

        public BannerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Banner updateObj)
        {
            var objFromDb = _db.Banner.FirstOrDefault(s => s.Id == updateObj.Id);

            objFromDb.Header = updateObj.Header;
            objFromDb.Body = updateObj.Body;
            objFromDb.Body = updateObj.Body;
            if (updateObj.Image != null)
            {
                objFromDb.Image = updateObj.Image;
            }

            _db.SaveChanges();
        }
    }
}
