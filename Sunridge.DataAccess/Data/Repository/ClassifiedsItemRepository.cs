using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClassifiedsItemRepository : Repository<ClassifiedsItem>, IClassifiedsItemRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassifiedsItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassifiedsItem classifieds)
        {
            var classifiedsFromDb = _db.Classifieds.FirstOrDefault(c => c.Id == classifieds.Id);

            classifiedsFromDb.Category = classifieds.Category;
            classifiedsFromDb.Subcategory = classifieds.Subcategory;
            classifiedsFromDb.Title = classifieds.Title;
            classifiedsFromDb.Description = classifieds.Description;
            classifiedsFromDb.Price = classifieds.Price;
            classifiedsFromDb.Phone = classifieds.Phone;
            classifiedsFromDb.Email = classifieds.Email;
            classifiedsFromDb.Website = classifieds.Website;
            classifiedsFromDb.Images = classifieds.Images;
        }

    }
}
