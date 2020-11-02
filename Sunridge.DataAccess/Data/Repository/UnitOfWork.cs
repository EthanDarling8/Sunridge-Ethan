using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //Inject an instance of our database service
        //underscore (_db) is to indicate it is read only        
        private readonly ApplicationDbContext _db;

        // **** Model IRepositories go here ****
        public ILotRepository Lot { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public ILotFileRepository LotFile { get; private set; }
        public ILot_ApplicationUserRepository Lot_ApplicationUser { get; private set; }
        public ILot_InventoryRepository Lot_Inventory { get; private set; }


        //ctor tab tab makes the constructor
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            // **** Models are instantiated here ****
            Lot = new LotRepository(_db);
            LotFile = new LotFileRepository(_db);
            Inventory = new InventoryRepository(_db);
            Lot_ApplicationUser = new Lot_ApplicationUserRepository(_db);
            Lot_Inventory = new Lot_InventoryRepository(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
