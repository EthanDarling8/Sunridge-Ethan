using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository {
    public interface IFireInfoRepository : IRepository<FireInfo> {
        void Update(FireInfo updateObj);
    }
}