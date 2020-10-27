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
        public IBannerRepository Banner { get; private set; }
        public IClassifiedsRepository Classifieds { get; private set; }
        public IViewCountRepository ViewCount { get; private set; }



        //ctor tab tab makes the constructor
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            // **** Models are instantiated here ****
            Lot = new LotRepository(_db);
            Banner = new BannerRepository(_db);
            Classifieds = new ClassifiedsRepository(_db);
            ViewCount = new ViewCountRepository(_db);
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
