using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IFormsRepository : IRepository<Forms>
    {        
        void Update(Forms updateObj);
    }
}
