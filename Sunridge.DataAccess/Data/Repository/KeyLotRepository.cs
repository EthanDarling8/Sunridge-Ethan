using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class KeyLotRepository : Repository<KeyLot>, IKeyLotRepository
    {
        private readonly ApplicationDbContext _db;

        public KeyLotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(KeyLot keyLot)
        {
            var objFromDb = _db.KeyLot.FirstOrDefault(q => q.Id == keyLot.Id);

            objFromDb.Issued = keyLot.Issued;
            objFromDb.ReturnDate = keyLot.ReturnDate;

            _db.SaveChanges();
        }
    }
}
