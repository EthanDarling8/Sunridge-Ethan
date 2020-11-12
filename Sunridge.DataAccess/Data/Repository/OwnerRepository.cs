using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    internal class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        private ApplicationDbContext _db;

        public OwnerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        
        public IEnumerable<SelectListItem> GetOwnerListForDropdown() {
            return _db.Owner.Select(i => new SelectListItem() {
                Text = i.FullName,
                Value = i.Id.ToString()
            });
        }
    }
}
