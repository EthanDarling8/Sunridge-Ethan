using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class PhotoCategoryRepository : Repository<PhotoCategory>, IPhotoCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public PhotoCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(PhotoCategory photoCategory)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.PhotoCategory.FirstOrDefault(s => s.Id == photoCategory.Id);

            // **** ToDo ****

            _db.SaveChanges();
        }
    }
}
