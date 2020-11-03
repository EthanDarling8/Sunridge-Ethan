using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.IRepository;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IPhotoCategoryRepository : IRepository<PhotoCategory>
    {
        IEnumerable<SelectListItem> GetListForDropDown();

        //Update is done here rather than in the master IRepository
        //because there is no generic update.
        //Update is specific to the object/table being updated.
        void Update(PhotoCategory photoCategory);
    }
}
