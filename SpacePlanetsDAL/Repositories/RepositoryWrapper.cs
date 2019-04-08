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
        private IRepositoryBase<StarSystem> _starSystemRepository;
        private IRepositoryBase<SpaceObject> _spaceObjectRepository;
        private IRepositoryBase<WebSession> _webSessionRepository;
        private IRepositoryBase<Player> _playerRepository;
        private IRepositoryBase<AccessToken> _accessTokenRepository;
        private IRepositoryBase<Character> _characterRepository;

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

        public IRepositoryBase<StarSystem> StarSystemRepository
        {
            get
            {
                if (_starSystemRepository == null)
                {
                    _starSystemRepository = new RepositoryBase<StarSystem>(_mongoClient);
                }
                return _starSystemRepository;
            }
        }

        public IRepositoryBase<SpaceObject> SpaceObjectRepository
        {
            get
            {
                if (_spaceObjectRepository == null)
                {
                    _spaceObjectRepository = new RepositoryBase<SpaceObject>(_mongoClient);
                }
                return _spaceObjectRepository;
            }
        }

        public IRepositoryBase<WebSession> WebSessionRepository
        {
            get
            {
                if (_webSessionRepository == null)
                {
                    _webSessionRepository = new RepositoryBase<WebSession>(_mongoClient);
                }
                return _webSessionRepository;
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

        public IRepositoryBase<AccessToken> AccessTokenRepository
        {
            get
            {
                if (_accessTokenRepository == null)
                {
                    _accessTokenRepository = new RepositoryBase<AccessToken>(_mongoClient);
                }
                return _accessTokenRepository;
            }
        }

        public IRepositoryBase<Character> CharacterRepository
        {
            get
            {
                if (_characterRepository == null)
                {
                    _characterRepository = new RepositoryBase<Character>(_mongoClient);
                }
                return _characterRepository;
            }
        }

    }
}
