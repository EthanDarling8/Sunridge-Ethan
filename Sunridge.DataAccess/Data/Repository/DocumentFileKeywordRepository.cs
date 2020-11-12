using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class DocumentFileKeywordRepository : Repository<DocumentFileKeyword>, IDocumentFileKeywordRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public DocumentFileKeywordRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }




        public void Update(DocumentFileKeyword documentFileKeyword)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.DocumentFileKeyword.FirstOrDefault(s => s.Id == documentFileKeyword.Id);

            objFromDb.Keyword = documentFileKeyword.Keyword;

            _db.SaveChanges();
        }
    }
}