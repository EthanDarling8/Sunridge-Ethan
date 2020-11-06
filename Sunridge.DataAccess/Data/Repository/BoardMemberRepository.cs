using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository {
    public class BoardMemberRepository : Repository<BoardMember>, IBoardMemberRepository {
        private readonly ApplicationDbContext _db;

        public BoardMemberRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetBoardMemberListForDropdown() {
            return _db.BoardMember.Select(i => new SelectListItem() {
                Text = i.Owner.FullName,
                Value = i.Id.ToString()
            });
        }

        public void Update(BoardMember updateObj) {
            var objFromDb = _db.BoardMember.FirstOrDefault(b => b.Id == updateObj.Id);

            objFromDb.Position = updateObj.Position;
            objFromDb.OwnerId = updateObj.OwnerId;
            objFromDb.Owner = updateObj.Owner;

            if (updateObj.Image != null) {
                objFromDb.Image = updateObj.Image;
            }

            _db.SaveChanges();
        }
    }
}