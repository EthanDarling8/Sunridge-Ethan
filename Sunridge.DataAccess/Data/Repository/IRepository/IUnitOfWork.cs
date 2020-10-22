using System;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    //Actually connects to the database for saving, updating, etc...
    public interface IUnitOfWork : IDisposable
    {
        // **** Model IRepositories go here ****
        ILotRepository Lot { get; }
        ILostItemRepository LostItem { get; }
        
        void Save();
    }
}
