using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LostItemRepository : Repository<LostItem>, ILostItemRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public LostItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(LostItem LostItem)
        {
            var objFromDb = _db.LostItem.FirstOrDefault(u => u.Id == LostItem.Id);
            if (LostItem.Image != null)                                             
            {
                objFromDb.Image = LostItem.Image;
            }
            objFromDb.Date = LostItem.Date;
            objFromDb.Image = LostItem.Image;
            objFromDb.Name = LostItem.Name;
            objFromDb.Description = LostItem.Description;
            //objFromDb.UserId = LostItem.UserId;

            _db.SaveChanges();                                                      
        }
    }
}
