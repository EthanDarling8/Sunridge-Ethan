using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILot_OwnerRepository : IRepository<Lot_Owner>
    {        
        void Update(Lot_Owner Lot_Owner);
    }
}
