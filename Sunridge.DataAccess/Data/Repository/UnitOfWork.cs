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
        public IViewCountRepository ViewCount { get; private set; }
        public IClassifiedsCategoryRepository ClassifiedsCategory { get; private set; }
        public IClassifiedsSubcategoryRepository ClassifiedsSubcategory { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IClassifiedsItemRepository ClassifiedsItem { get; private set; }





        //ctor tab tab makes the constructor
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            // **** Models are instantiated here ****
            Lot = new LotRepository(_db);
            Banner = new BannerRepository(_db);
            ClassifiedsItem = new ClassifiedsItemRepository(_db);
            ViewCount = new ViewCountRepository(_db);
            ClassifiedsCategory = new ClassifiedsCategoryRepository(_db);
            ClassifiedsSubcategory = new ClassifiedsSubcategoryRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);

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
