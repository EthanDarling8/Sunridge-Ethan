using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        private readonly ApplicationDbContext _db;

        public AssetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Asset asset)
        {
            var objFromDb = _db.Asset.FirstOrDefault(a => a.Id == asset.Id);

            objFromDb.Name = asset.Name;
            objFromDb.Price = asset.Price;       
            objFromDb.Description = asset.Description;
            objFromDb.Status = asset.Status;
            objFromDb.Date = asset.Date;

            _db.SaveChanges();
        }
    }
}
