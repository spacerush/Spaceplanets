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
            Session.SaveChanges();
        }

        public T GetOne<T1>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Session.Query<T>().Where<T>(whereCondition).SingleOrDefault();
        }

        public List<T> GetAll<T1>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Session.Query<T>().Where<T>(whereCondition).ToList();
        }

        public void UpdateOne<T1>(T1 entity)
        {
            Session.Update<T1>(entity);
            Session.SaveChanges();
        }

        public void AddMany<T1>(List<T1> entities)
        {
            foreach (var item in entities)
            {
                Session.Insert<T1>(item);
            }
            Session.SaveChanges();
        }

        public int Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Session.Query<T>().Where<T>(whereCondition).Count();
        }
    }
}
