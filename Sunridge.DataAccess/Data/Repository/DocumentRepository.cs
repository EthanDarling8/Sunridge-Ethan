using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public DocumentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }




        public void Update(Document document)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Document.FirstOrDefault(s => s.Id == document.Id);

            objFromDb.Name = document.Name;
            objFromDb.Text = document.Text;
            objFromDb.File = document.File;

            _db.SaveChanges();
        }
    }
}