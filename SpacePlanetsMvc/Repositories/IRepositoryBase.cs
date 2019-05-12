using Marten;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories
{
    public interface IRepositoryBase<T>
    {
        void AddOne<T1>(T1 entity);

        T GetOne<T1>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
        void UpdateOne<T1>(T1 entity);

        List<T> GetAll<T1>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
    }
}
