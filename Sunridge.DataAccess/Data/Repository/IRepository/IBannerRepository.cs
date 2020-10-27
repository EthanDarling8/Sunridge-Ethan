using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IBannerRepository : IRepository<Banner>
    {        
        void Update(Banner updateObj);
    }
}
