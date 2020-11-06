using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository {
    public interface IBoardMemberRepository : IRepository<BoardMember> {
        IEnumerable<SelectListItem> GetBoardMemberListForDropdown();
        void Update(BoardMember updateObj);
    }
}