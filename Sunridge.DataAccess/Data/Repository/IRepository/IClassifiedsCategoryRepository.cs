using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedsCategoryRepository : IRepository<ClassifiedsCategory>
    {
        IEnumerable<SelectListItem> GetClassifiedsCategoryListForDropDown();

        void Update(ClassifiedsCategory classifiedsCategory);
    }
}
