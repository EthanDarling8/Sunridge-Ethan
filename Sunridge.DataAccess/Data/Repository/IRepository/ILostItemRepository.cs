using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILostItemRepository : IRepository<LostItem>
    {
        void Update(LostItem lostItem);
    }
}
