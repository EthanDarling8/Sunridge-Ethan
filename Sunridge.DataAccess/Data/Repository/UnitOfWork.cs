using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //Inject an instance of our database service      
        private readonly ApplicationDbContext _db;


        // **** Model IRepositories go here ****
        public IBannerRepository Banner { get; private set; }
        public IOwnerRepository Owner { get; private set; }
        public INewsRepository News { get; private set; }
        public IFireInfoRepository FireInfo { get; private set; }
        
        // Common Area Asset Repositories
        public IAssetRepository Asset { get; private set; }
        public IMaintenanceRecordRepository MaintenanceRecord { get; private set; }
        
        public IFormsRepository Forms { get; private set; }

        // Board Member IRepositories
        public IOwnerBoardMemberRepository OwnerBoardMember { get; private set; }
        public IBoardMemberRepository BoardMember { get; private set; }

        // Classifieds IRepositories
        public IClassifiedsCategoryRepository CategoryRepository { get; private set; }
        public IClassifiedsItemRepository ClassifiedsItemRepository { get; private set; }
        
        //Documents IRepositories
        public IDocumentCategoryRepository DocumentCategory { get; private set; }
        public IDocumentFileRepository DocumentFile { get; private set; }
        public IDocumentFileKeywordRepository DocumentFileKeyword { get; private set; }
        public IDocumentSectionRepository DocumentSection { get; private set; }
        public IDocumentSectionTextRepository DocumentSectionText { get; private set; }

        //Photo Gallery IRepositories
        public IPhotoCategoryRepository PhotoCategory { get; private set; }
        public IPhotoAlbumRepository PhotoAlbum { get; private set; }
        public IPhotoRepository Photo { get; private set; }

        //Classifieds IRepositories
        public IClassifiedsItemRepository ClassifiedsItem { get; private set; }
        public IClassifiedsCategoryRepository ClassifiedsCategory { get; private set; }
       // public IClassifiedsSubcategoryRepository ClassifiedsSubcategory { get; private set; }

        // Key IRepositories
        public IKeyRepository Key { get; private set; }
        public IKeyLotRepository KeyLot { get; private set; }

        // Lot IRepositories
        public ILotRepository Lot { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public ILotFileRepository LotFile { get; private set; }
        public ILot_OwnerFileRepository Lot_OwnerFile { get; private set; }
        public ILot_OwnerRepository Lot_Owner { get; private set; }
        public ILot_InventoryRepository Lot_Inventory { get; private set; }

        // Lost & Found IRepositories
        public ILostItemRepository LostItem { get; private set; }

        public IViewCountRepository ViewCount { get; private set; }
        


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            // **** Models are instantiated here ****         
            Banner = new BannerRepository(_db);
            Owner = new OwnerRepository(_db);
            News = new NewsRepository(_db);
            FireInfo = new FireInfoRepository(_db);

            // Common Area Asset Models
            Asset = new AssetRepository(_db);
            MaintenanceRecord = new MaintenanceRecordRepository(_db);

            Forms = new FormsRepository(_db);

            // Board Member Models
            BoardMember = new BoardMemberRepository(_db);
            OwnerBoardMember = new OwnerBoardMemberRepository(_db);

            //Documents Models
            DocumentCategory = new DocumentCategoryRepository(_db);
            DocumentFile = new DocumentFileRepository(_db);
            DocumentFileKeyword = new DocumentFileKeywordRepository(_db);
            DocumentSection = new DocumentSectionRepository(_db);
            DocumentSectionText = new DocumentSectionTextRepository(_db);           
            
            //Photo Gallery Models
            PhotoCategory = new PhotoCategoryRepository(_db);
            PhotoAlbum = new PhotoAlbumRepository(_db);
            Photo = new PhotoRepository(_db);

            //Classifieds Models
            ClassifiedsCategory = new ClassifiedsCategoryRepository(_db);
            //ClassifiedsSubcategory = new ClassifiedsSubcategoryRepository(_db);
            ClassifiedsItem = new ClassifiedsItemRepository(_db);

            // Key Models
            Key = new KeyRepository(_db);
            KeyLot = new KeyLotRepository(_db);

            // Lot Models
            Lot = new LotRepository(_db);
            LotFile = new LotFileRepository(_db);
            Lot_OwnerFile = new Lot_OwnerFileRepository(_db);
            Inventory = new InventoryRepository(_db);
            Lot_Owner = new Lot_OwnerRepository(_db);
            Lot_Inventory = new Lot_InventoryRepository(_db);
         
            // Lost & Found Models
            LostItem = new LostItemRepository(_db);
            
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
