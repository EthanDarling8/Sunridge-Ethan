using Sunridge.Models;
using Sunridge.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
