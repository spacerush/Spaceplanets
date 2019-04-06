using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ViewRepositoryBase<T> : IViewRepositoryBase<T> where T : class
    {
        protected Model.GamedatabaseContext Context { get; set; }

        public ViewRepositoryBase(Model.GamedatabaseContext context)
        {
            this.Context = context;
        }

        public IEnumerable<T> FindAll()
        {
            return this.Context.Query<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.Context.Query<T>().Where(expression);
        }

    }
}
