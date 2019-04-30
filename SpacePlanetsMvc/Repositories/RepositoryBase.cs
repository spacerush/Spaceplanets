using MongoDB.Driver;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories
{
    public class RepositoryBase<T> : BaseMongoRepository, IRepositoryBase<T> where T : class
    {
        private readonly IMongoClient _client;

        public RepositoryBase(IMongoClient client) : base(client.GetDatabase("SpacePlanets"))
        {
            _client = client;
        }

    }
}
