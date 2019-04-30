using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories
{
    public interface IRepositoryBase<T> : IBaseMongoRepository
    {

    }
}
