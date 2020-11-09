using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class Lot_InventoryRepository : Repository<Lot_Inventory>, ILot_InventoryRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public Lot_InventoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Lot_Inventory Lot_Inventory)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Lot_Inventory.FirstOrDefault(s => s.Id == Lot_Inventory.Id);

            // **** ToDo ****

            _db.SaveChanges();
        }
    }
}
