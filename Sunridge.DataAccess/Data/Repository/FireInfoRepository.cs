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
            objFromDb.Content = updateObj.Content;
            objFromDb.DisplayDate = updateObj.DisplayDate;
            objFromDb.Archived = updateObj.Archived;
            _db.SaveChanges();
        }
    }
}