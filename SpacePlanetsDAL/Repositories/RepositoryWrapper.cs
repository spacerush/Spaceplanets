using MongoDB.Driver;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IMongoClient _mongoClient;

        private IRepositoryBase<Galaxy> _galaxyRepository;
        private IRepositoryBase<Player> _playerRepository;

        public RepositoryWrapper(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IRepositoryBase<Galaxy> GalaxyRepository
        {
            get
            {
                if (_galaxyRepository == null)
                {
                    _galaxyRepository = new RepositoryBase<Galaxy>(_mongoClient);
                }
                return _galaxyRepository;
            }
        }

        public IRepositoryBase<Player> PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    _playerRepository = new RepositoryBase<Player>(_mongoClient);
                }
                return _playerRepository;
            }
        }

    }
}
