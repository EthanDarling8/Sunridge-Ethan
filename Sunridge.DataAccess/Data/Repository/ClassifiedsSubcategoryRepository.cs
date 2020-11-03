using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClassifiedsSubcategoryRepository : Repository<ClassifiedsSubcategory>, IClassifiedsSubcategoryRepository
    {
        private readonly ApplicationDbContext _db;


        public ClassifiedsSubcategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetClassifiedsSubcategoryListForDropDown()
        {
            return _db.ClassifiedsSubcategory.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

        }

        public void Update(ClassifiedsSubcategory classifiedsSubcategory)
        {
            var objFromDb = _db.ClassifiedsSubcategory.FirstOrDefault(s => s.Id == classifiedsSubcategory.Id);

            objFromDb.Name = classifiedsSubcategory.Name;

        }
    }
}

