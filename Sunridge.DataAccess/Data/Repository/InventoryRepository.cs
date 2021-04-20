using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public InventoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Inventory inventory)
        {
            var objFromDb = _db.Inventory.FirstOrDefault(u => u.Id == inventory.Id);
            objFromDb.ItemName = inventory.ItemName;

            _db.SaveChanges();
        }
        public IEnumerable<SelectListItem> GetInventoryList()
        {
            return _db.Inventory.Select(i => new SelectListItem()
            {
                Text = i.ItemName,
                Value = i.Id.ToString()
            });
        }
    }
}
