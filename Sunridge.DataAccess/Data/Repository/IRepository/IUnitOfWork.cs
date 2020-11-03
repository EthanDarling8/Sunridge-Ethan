using System;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    //Actually connects to the database for saving, updating, etc...
    public interface IUnitOfWork : IDisposable
    {
        // **** Model IRepositories go here ****
        ILotRepository Lot { get; }
        IBannerRepository Banner { get; }
        IApplicationUserRepository ApplicationUser { get; }

        //Photo Gallery Repositories
        IPhotoCategoryRepository PhotoCategory { get; }
        IPhotoAlbumRepository PhotoAlbum { get; }
        IPhotoRepository Photo { get; }

        //Documents Repositories
        IDocumentCategoryRepository DocumentCategory { get; }
        IDocumentRepository Document { get; }

        void Save();
    }
}
