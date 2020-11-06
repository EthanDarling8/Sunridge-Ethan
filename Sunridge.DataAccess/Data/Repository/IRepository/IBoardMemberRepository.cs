using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository {
    public interface IBoardMemberRepository : IRepository<BoardMember> {
        void Update(BoardMember updateObj);

    }
}