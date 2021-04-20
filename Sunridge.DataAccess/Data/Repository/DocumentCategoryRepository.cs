using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class DocumentCategoryRepository : Repository<DocumentCategory>, IDocumentCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public DocumentCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }




        public IEnumerable<SelectListItem> GetListForDropDown()
        {
            return _db.DocumentCategory.Select(i => new SelectListItem()
            {
                Text = i.Name,
                //Id is used for url routing in the page
                Value = i.Id.ToString()
            }).OrderBy(c => c.Text);
        }




        public void Update(DocumentCategory documentCategory)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.DocumentCategory.FirstOrDefault(s => s.Id == documentCategory.Id);

            objFromDb.Name = documentCategory.Name;

            _db.SaveChanges();
        }
    }
}