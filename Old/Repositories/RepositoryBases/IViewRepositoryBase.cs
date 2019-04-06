using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public interface IViewRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
