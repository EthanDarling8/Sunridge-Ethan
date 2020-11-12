using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class MaintenanceRecordRepository : Repository<MaintenanceRecord>, IMaintenanceRecordRepository
    {
        private readonly ApplicationDbContext _db;

        public MaintenanceRecordRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MaintenanceRecord maintenanceRecord)
        {
            var objFromDb = _db.MaintenanceRecord.FirstOrDefault(mr => mr.Id == maintenanceRecord.Id);

            objFromDb.Description = maintenanceRecord.Description;
            objFromDb.Cost = maintenanceRecord.Cost;
            objFromDb.DateCompleted = maintenanceRecord.DateCompleted;
            objFromDb.AssetId = maintenanceRecord.AssetId;

            _db.SaveChanges();
        }
    }
}
