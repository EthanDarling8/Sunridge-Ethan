using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LotFileRepository : Repository<LotFile>, ILotFileRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public LotFileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(LotFile LotFile)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.LotFile.FirstOrDefault(s => s.Id == LotFile.Id);

            objFromDb.Title = LotFile.Title;
            objFromDb.Description = LotFile.Description;
            objFromDb.File = LotFile.File;

            _db.SaveChanges();
        }
    }
}
