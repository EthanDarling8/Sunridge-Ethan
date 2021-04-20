using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClassifiedsImagesRepository : Repository<ClassifiedsImages>, IClassifiedsImagesRepository
    {
        private readonly ApplicationDbContext _db;


        public ClassifiedsImagesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassifiedsImages classifiedsImages)
        {
            var classifiedsImagesFromDb = _db.ClassifiedsImages.FirstOrDefault(c => c.Id == classifiedsImages.Id);

            classifiedsImagesFromDb.ImagePath = classifiedsImages.ImagePath;
            classifiedsImagesFromDb.ClassifiedsItemId = classifiedsImages.ClassifiedsItemId;


        }
    }
}
