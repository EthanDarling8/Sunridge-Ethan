using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        private readonly ApplicationDbContext _db;

        public NewsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(News updateObj)
        {
            var objFromDb = _db.News.FirstOrDefault(s => s.Id == updateObj.Id);

            objFromDb.Title = updateObj.Title;
            objFromDb.Content = updateObj.Content;
            objFromDb.DisplayDate = updateObj.DisplayDate;
            objFromDb.Archived = updateObj.Archived;
            _db.SaveChanges();
        }
    }
}
