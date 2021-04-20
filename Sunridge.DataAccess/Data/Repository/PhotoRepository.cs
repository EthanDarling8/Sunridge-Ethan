using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public PhotoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Photo photo)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Photo.FirstOrDefault(s => s.Id == photo.Id);

            if(photo.Title != null)
            {
                objFromDb.Title = photo.Title;
            }

            if(photo.Description != null)
            {
                objFromDb.Description = photo.Description;
            }
            
            objFromDb.Image = photo.Image;
            objFromDb.Thumb = photo.Thumb;

            _db.SaveChanges();
        }
    }
}
