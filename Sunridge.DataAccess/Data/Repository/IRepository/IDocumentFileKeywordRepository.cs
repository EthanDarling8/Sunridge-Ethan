using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IDocumentFileKeywordRepository : IRepository<DocumentFileKeyword>
    {
        //Update is done here rather than in the master IRepository
        //because there is no generic update.
        //Update is specific to the object/table being updated.
        void Update(DocumentFileKeyword documentFileKeyword);
    }
}
