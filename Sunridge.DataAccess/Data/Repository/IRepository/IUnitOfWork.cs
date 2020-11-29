using System;

namespace Sunridge.DataAccess.Data.Repository.IRepository {
    //Actually connects to the database for saving, updating, etc...
    public interface IUnitOfWork : IDisposable {
        // **** Model IRepositories go here ****
        IBannerRepository Banner { get; }
        IOwnerRepository Owner { get; }
        INewsRepository News { get; }
        IFireInfoRepository FireInfo { get; }
        IFormsRepository Forms { get; }

        // Board Member Repositories
        IBoardMemberRepository BoardMember { get; }
        IOwnerBoardMemberRepository OwnerBoardMember { get; }

        //Documents Repositories
        IDocumentCategoryRepository DocumentCategory { get; }
        IDocumentFileRepository DocumentFile { get; }
        IDocumentFileKeywordRepository DocumentFileKeyword { get; }
        IDocumentSectionRepository DocumentSection { get; }
        IDocumentSectionTextRepository DocumentSectionText { get; }


        //Photo Gallery Repositories
        IPhotoCategoryRepository PhotoCategory { get; }
        IPhotoAlbumRepository PhotoAlbum { get; }
        IPhotoRepository Photo { get; }

        // Key Repositories
        IKeyRepository Key { get; }
        IKeyLotRepository KeyLot { get; }

        // Lot Repositories
        ILotRepository Lot { get; }
        ILotFileRepository LotFile { get; }
        ILot_OwnerFileRepository Lot_OwnerFile { get; }
        IInventoryRepository Inventory { get; }
        ILot_OwnerRepository Lot_Owner { get; }
        ILot_InventoryRepository Lot_Inventory { get; }

        // Lost And Found Repositories
        ILostItemRepository LostItem { get; }

        
        //Classifieds Repositories
        IClassifiedsItemRepository ClassifiedsItem { get; }
        IClassifiedsCategoryRepository ClassifiedsCategory { get; }
        //IClassifiedsSubcategoryRepository ClassifiedsSubcategory { get; }

       

    void Save();
    }
}