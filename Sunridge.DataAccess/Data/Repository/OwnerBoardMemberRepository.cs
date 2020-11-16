using System.Linq;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository {
    public class OwnerBoardMemberRepository : Repository<OwnerBoardMember>, IOwnerBoardMemberRepository {
        private readonly ApplicationDbContext _db;

        public OwnerBoardMemberRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(OwnerBoardMember ownerBoardMember) {
            var objFromDb = _db.OwnerBoardMember.FirstOrDefault(s => s.Id == ownerBoardMember.Id);

            objFromDb.Owner = ownerBoardMember.Owner;
            objFromDb.BoardMember = ownerBoardMember.BoardMember;
            
            _db.SaveChanges();
        }
    }
}