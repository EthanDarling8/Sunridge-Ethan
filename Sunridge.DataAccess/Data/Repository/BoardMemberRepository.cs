using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository {
    public class BoardMemberRepository : Repository<BoardMember>, IBoardMemberRepository {
        private readonly ApplicationDbContext _db;
        
        public BoardMemberRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(BoardMember updateObj) {
            var objFromDb = _db.BoardMember.FirstOrDefault(s => s.Id == updateObj.Id);

            objFromDb.Image = updateObj.Image;
            objFromDb.Position = updateObj.Position;

            _db.SaveChanges(); 
        }
    }
}