using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface INewsRepository : IRepository<News>
    {        
        void Update(News updateObj);
    }
}
