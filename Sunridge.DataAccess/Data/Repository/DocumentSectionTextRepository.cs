using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class DocumentSectionTextRepository : Repository<DocumentSectionText>, IDocumentSectionTextRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public DocumentSectionTextRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }




        public void Update(DocumentSectionText documentSectionText)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.DocumentSectionText.FirstOrDefault(s => s.Id == documentSectionText.Id);

            objFromDb.Name = documentSectionText.Name;
            objFromDb.DisplayOrder = documentSectionText.DisplayOrder;
            objFromDb.Text = documentSectionText.Text;

            _db.SaveChanges();
        }
    }
}