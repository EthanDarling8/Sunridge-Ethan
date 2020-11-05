using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class Lot_OwnerRepository : Repository<Lot_Owner>, ILot_OwnerRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public Lot_OwnerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Lot_Owner Lot_Owner)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Lot_Owner.FirstOrDefault(s => s.Id == Lot_Owner.Id);

            // **** ToDo ****

            _db.SaveChanges();
        }
    }
}
