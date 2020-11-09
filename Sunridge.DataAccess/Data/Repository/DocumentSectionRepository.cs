using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class DocumentSectionRepository : Repository<DocumentSection>, IDocumentSectionRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public DocumentSectionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }




        public IEnumerable<SelectListItem> GetListForDropDown()
        {
            return _db.DocumentSection.Select(i => new SelectListItem()
            {
                Text = i.Name,
                //Id is used for url routing in the page
                Value = i.Id.ToString()
            }).OrderBy(s => s.Text);
        }




        public void Update(DocumentSection documentSection)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.DocumentSection.FirstOrDefault(s => s.Id == documentSection.Id);

            objFromDb.DocumentCategoryId = documentSection.DocumentCategoryId;
            objFromDb.Name = documentSection.Name; 
            objFromDb.DisplayOrder = documentSection.DisplayOrder;

            _db.SaveChanges();
        }
    }
}