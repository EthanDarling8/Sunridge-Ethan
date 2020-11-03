using System;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    //Actually connects to the database for saving, updating, etc...
    public interface IUnitOfWork : IDisposable
    {
        // **** Model IRepositories go here ****
        ILotRepository Lot { get; }
        IBannerRepository Banner { get; }
        IClassifiedsItemRepository ClassifiedsItem { get; }
        IViewCountRepository ViewCount { get; }
        

        IApplicationUserRepository ApplicationUser { get;  }
        IClassifiedsCategoryRepository ClassifiedsCategory { get;  }
        IClassifiedsSubcategoryRepository ClassifiedsSubcategory { get;  }

        void Save();
    }
}
