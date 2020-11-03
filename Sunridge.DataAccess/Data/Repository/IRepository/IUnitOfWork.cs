using System;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    //Actually connects to the database for saving, updating, etc...
    public interface IUnitOfWork : IDisposable
    {
        // **** Model IRepositories go here ****
        IBannerRepository Banner { get; }
        IApplicationUserRepository ApplicationUser { get; }
        INewsRepository News { get; }

        //Photo Gallery Repositories
        IPhotoCategoryRepository PhotoCategory { get; }
        IPhotoAlbumRepository PhotoAlbum { get; }
        IPhotoRepository Photo { get; }

        // Lot Repositories
        ILotRepository Lot { get; }
        ILotFileRepository LotFile { get; }
        IInventoryRepository Inventory { get; }
        ILot_ApplicationUserRepository Lot_ApplicationUser { get; }
        ILot_InventoryRepository Lot_Inventory { get; }

        
        void Save();
    }
}
