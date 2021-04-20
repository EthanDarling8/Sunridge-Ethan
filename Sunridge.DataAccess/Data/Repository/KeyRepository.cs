using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class KeyRepository : Repository<Key>, IKeyRepository
    {
        private readonly ApplicationDbContext _db;

        public KeyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
