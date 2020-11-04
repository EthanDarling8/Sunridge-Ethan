using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class Lot_OwnerFileRepository : Repository<Lot_OwnerFile>, ILot_OwnerFileRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public Lot_OwnerFileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Lot_OwnerFile Lot_OwnerFile)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Lot_OwnerFile.FirstOrDefault(s => s.Id == Lot_OwnerFile.Id);

            // **** ToDo ****

            _db.SaveChanges();
        }
    }
}
