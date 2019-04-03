using SpaceRushEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected GamedatabaseContext SrContext { get; set; }

        public RepositoryBase(GamedatabaseContext srContext)
        {
            this.SrContext = srContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.SrContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.SrContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            this.SrContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.SrContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.SrContext.Set<T>().Remove(entity);
        }

        public T FindRandom()
        {
            return this.SrContext.Set<T>().OrderBy(o => Guid.NewGuid()).Take(1).Single();
        }
    }
}
