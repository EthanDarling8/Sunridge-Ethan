using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class DocumentFileRepository : Repository<DocumentFile>, IDocumentFileRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public DocumentFileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }




        public void Update(DocumentFile documentFile)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.DocumentFile.FirstOrDefault(s => s.Id == documentFile.Id);

            objFromDb.DocumentCategoryId = documentFile.DocumentCategoryId;
            objFromDb.Name = documentFile.Name;
            objFromDb.Extension = documentFile.Extension;
            objFromDb.Description = documentFile.Description;
            objFromDb.PublishedDate = documentFile.PublishedDate;
            objFromDb.DisplayOrder = documentFile.DisplayOrder;
            objFromDb.File = documentFile.File;

            _db.SaveChanges();
        }
    }
}