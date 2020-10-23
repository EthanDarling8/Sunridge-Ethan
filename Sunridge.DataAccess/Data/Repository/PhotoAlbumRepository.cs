using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class PhotoAlbumRepository : Repository<PhotoAlbum>, IPhotoAlbumRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public PhotoAlbumRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(PhotoAlbum photoAlbum)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.PhotoAlbum.FirstOrDefault(s => s.Id == photoAlbum.Id);

            // **** ToDo ****

            _db.SaveChanges();
        }
    }
}
