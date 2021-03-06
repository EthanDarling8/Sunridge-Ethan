using Sunridge.DataAccess.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedsItemRepository : IRepository<ClassifiedsItem>
    {
        void Update(ClassifiedsItem classifieds);
        public void Increment(ClassifiedsItem classifiedsItem, int viewCount);
    }
}
