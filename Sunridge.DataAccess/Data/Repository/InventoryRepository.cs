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


        public void Update(Inventory Inventory)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Inventory.FirstOrDefault(s => s.Id == Inventory.Id);

            // **** ToDo ****

            _db.SaveChanges();
        }
        public IEnumerable<SelectListItem> GetInventoryListForDropDown()
        {
            return _db.Inventory.Select(i => new SelectListItem()
            {
                Text = i.ItemName,
                Value = i.Id.ToString()
            });
        }
    }
}
