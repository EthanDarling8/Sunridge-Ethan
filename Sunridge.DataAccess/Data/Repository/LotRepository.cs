using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public LotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Lot lot)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Lot.FirstOrDefault(s => s.Id == lot.Id);

            objFromDb.LotNumber = lot.LotNumber;
            objFromDb.Address = lot.Address;
            objFromDb.TaxId = lot.TaxId;

            _db.SaveChanges();
        }
    }
}
