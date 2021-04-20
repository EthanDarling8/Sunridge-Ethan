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
            objFromDb.Name = LostItem.Name;
            objFromDb.Date = LostItem.Date;
            objFromDb.ExpirationDate = LostItem.Date.AddDays(30);
            objFromDb.Status = LostItem.Status;
            objFromDb.Description = LostItem.Description;
            objFromDb.Image = LostItem.Image;
            objFromDb.DisplayEmail = LostItem.DisplayEmail;
            objFromDb.DisplayPhone = LostItem.DisplayPhone;
            objFromDb.DisplayAddress = LostItem.DisplayAddress;
            objFromDb.OwnerId = LostItem.OwnerId;

            _db.SaveChanges();
        }
    }
}
