using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILot_ApplicationUserRepository : IRepository<Lot_ApplicationUser>
    {        
        //Update is done here rather than in the master IRepository
        //because there is no generic update.
        //Update is specific to the object/table being updated.
        void Update(Lot_ApplicationUser Lot_ApplicationUser);
    }
}
