using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.IRepository;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        IEnumerable<SelectListItem> GetOwnerListForDropDown();
    }
}
