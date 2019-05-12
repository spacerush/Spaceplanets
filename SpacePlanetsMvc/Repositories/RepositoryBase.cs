using AutoMapper;
using Marten;
using MongoDB.Driver;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IDocumentStore _client;
        public readonly IDocumentSession Session;


        public RepositoryBase(IDocumentStore client)
        {
            _client = client;
            Session = _client.OpenSession();
        }

        public void AddOne<T1>(T1 entity)
        {
            Session.Insert<T1>(entity);
        }

        public T GetOne<T1>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Session.Query<T>().Where<T>(whereCondition).Single();
        }

        public List<T> GetAll<T1>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Session.Query<T>().Where<T>(whereCondition).ToList();
        }

        public void UpdateOne<T1>(T1 entity)
        {
            Session.Update<T1>(entity);
        }
    }
}
