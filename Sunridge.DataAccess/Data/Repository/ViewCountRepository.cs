using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ViewCountRepository : Repository<ViewCount>, IViewCountRepository
    {
        private readonly ApplicationDbContext _db;

        public ViewCountRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int Increment(ViewCount viewCount, int count)
        {
            viewCount.Count += count;
            return viewCount.Count;
        }

    }
}
