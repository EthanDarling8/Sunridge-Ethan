using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository {
    public class FireInfoRepository : Repository<FireInfo>, IFireInfoRepository {
        private readonly ApplicationDbContext _db;

        public FireInfoRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(FireInfo updateObj) {
            var objFromDb = _db.FireInfo.FirstOrDefault(f => f.Id == updateObj.Id);

            objFromDb.Title = updateObj.Title;
            objFromDb.Description = updateObj.Description;
            objFromDb.Link1 = updateObj.Link1;
            objFromDb.Link2 = updateObj.Link2;
            objFromDb.Phone = updateObj.Phone;
            objFromDb.Email = updateObj.Email;
            objFromDb.PostDate = updateObj.PostDate;
            
            if (updateObj.File != null) {
                objFromDb.File = updateObj.File;
            }

            _db.SaveChanges();
        }
    }
}