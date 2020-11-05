using System;

namespace Sunridge.DataAccess.Data.Repository.IRepository {
    //Actually connects to the database for saving, updating, etc...
    public interface IUnitOfWork : IDisposable {
        // **** Model IRepositories go here ****
        ILotRepository Lot { get; }
        IBannerRepository Banner { get; }
        IOwnerRepository Owner { get; }
        INewsRepository News { get; }

        // Board Member
        IBoardMemberRepository BoardMember { get; }

        // Fire Info
        IFireInfoRepository FireInfo { get; }

        //Photo Gallery Repositories
        IPhotoCategoryRepository PhotoCategory { get; }
        IPhotoAlbumRepository PhotoAlbum { get; }
        IPhotoRepository Photo { get; }

        void Save();
    }
}