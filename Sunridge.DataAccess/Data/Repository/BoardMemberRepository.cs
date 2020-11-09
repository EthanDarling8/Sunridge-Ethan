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
            throw new System.NotImplementedException();
        }
    }
}