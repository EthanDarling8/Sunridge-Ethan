using Sunridge.DataAccess.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository {
    public interface IOwnerBoardMemberRepository: IRepository<OwnerBoardMember> {
        void Update(OwnerBoardMember ownerBoardMember);
    }
}