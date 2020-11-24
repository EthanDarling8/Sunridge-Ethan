using Sunridge.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sunridge.DataAccess.Data.Repository
{
    //Implementation of the IRepository
    public class Repository<T> : IRepository<T> where T : class
    {
        //Bring in an instance of DbContext (maps models to database)
        protected readonly DbContext Context;
        //Copy of table in database
        internal DbSet<T> dbset;

        //Instantiate Repository (constructor)
        public Repository(DbContext context)
        {
            Context = context;
            //Gets the table
            this.dbset = context.Set<T>();
        }

        public T Get(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            //Allows you to make changes without altering the master input
            IQueryable<T> query = dbset;

            //Should do include BEFORE anything else.
            //include properties are comma seperated. We need to remove the commas.
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[]
                    {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    //Include is the same as Join in SQL
                    //Making a character array from the original
                    query = query.Include(includeProperty);
                }
            }

            if (filter != null)
            {
                //Pass in the Where clause if there is one.
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }            

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            //Allows you to make changes without altering the master input
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                //Pass in the Where clause if there is one.
                query = query.Where(filter);
            }

            //include properties are comma seperated. We need to remove the commas.
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[]
                    {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    //Include is the same as Join in SQL
                    //Making a character array from the original
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Add(T entity)
        {
            //Stages changes only, does not save them.
            dbset.Add(entity);
        }

        public void Remove(int id)
        {
            //Get the whole object with the id
            T entityToRemove = dbset.Find(id);
            //Remove that object using our Remove fuction
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
