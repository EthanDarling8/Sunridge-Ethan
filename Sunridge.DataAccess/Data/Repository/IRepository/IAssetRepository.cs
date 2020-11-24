using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IAssetRepository : IRepository<Asset>
    {
        void Update(Asset asset);
    }
}
