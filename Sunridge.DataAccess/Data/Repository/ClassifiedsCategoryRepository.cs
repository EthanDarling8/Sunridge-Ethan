using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClassifiedsCategoryRepository : Repository<ClassifiedsCategory>, IClassifiedsCategoryRepository
    {
        private readonly ApplicationDbContext _db;


        public ClassifiedsCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetClassifiedsCategoryListForDropDown()
        {
            return _db.ClassifiedsCategory.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

        }

        public void Update(ClassifiedsCategory classifiedsCategory)
        {
            var objFromDb = _db.ClassifiedsCategory.FirstOrDefault(s => s.Id == classifiedsCategory.Id);

            objFromDb.Name = classifiedsCategory.Name;
            
        }
    }
}
