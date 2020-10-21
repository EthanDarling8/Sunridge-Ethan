using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sunridge.DataAccess.IRepository
{    
    public interface IRepository<T> where T : class
    {
        //Get object by Id (works for a student, category, etc.)
        T Get(int id);

        //Get all objects Ienumerable (allows for SQL clauses (where, order by, include other relations)
        IEnumerable<T> GetAll(
            //Where
            //Function takes in type T, returns bool, defaults to null
            //So, if the function is true, input the where clause into the expression
            Expression<Func<T, bool>> filter = null,
            //Order By
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            //Other relations (joins)
            string includeProperties = null
            );

        //Get the first or default
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        //Add an item
        void Add(T entity);

        //Remove by Id
        void Remove(int id);

        //Remove by object
        void Remove(T entity);

        //Remove range (list of objects)
        void RemoveRange(IEnumerable<T> entity);
    }
}
