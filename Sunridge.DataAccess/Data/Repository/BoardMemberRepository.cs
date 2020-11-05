using System.Linq;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository {
    public class BoardMemberRepository : Repository<BoardMember>, IBoardMemberRepository {
        private readonly ApplicationDbContext _db;

        public BoardMemberRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(BoardMember updateObj) {
            var objFromDb = _db.BoardMember.FirstOrDefault(b => b.Id == updateObj.Id);

            objFromDb.Position = updateObj.Position;
            
            if (updateObj.Image != null) {
                objFromDb.Image = updateObj.Image;
            }

            _db.SaveChanges();
        }
    }
}