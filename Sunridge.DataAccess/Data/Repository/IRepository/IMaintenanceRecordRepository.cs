using Sunridge.DataAccess.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IMaintenanceRecordRepository : IRepository<MaintenanceRecord>
    {
        void Update(MaintenanceRecord maintenanceRecord);
    }
}
