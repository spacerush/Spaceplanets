﻿using MongoDB.Driver;
using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IMongoClient _mongoClient;

        private IRepositoryBase<GalaxyContainer> _galaxyContainerRepository;
        private IRepositoryBase<SpaceObject> _spaceObjectRepository;
        private IRepositoryBase<WebSession> _webSessionRepository;
        private IRepositoryBase<Player> _playerRepository;
        private IRepositoryBase<AccessToken> _accessTokenRepository;
        private IRepositoryBase<Character> _characterRepository;

        private IRepositoryBase<Ship> _shipRepository;
        private IRepositoryBase<ShipTemplate> _shipTemplateRepository;
        private IRepositoryBase<ShipModule> _shipModuleRepository;

        #region character augmentation
        private IRepositoryBase<ImplantTemplate> _implantTemplateRepository;
        private IRepositoryBase<MicroclusterTemplate> _microclusterTemplateRepository;
        #endregion

        public RepositoryWrapper(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IRepositoryBase<GalaxyContainer> GalaxyContainerRepository
        {
            get
            {
                if (_galaxyContainerRepository == null)
                {
                    _galaxyContainerRepository = new RepositoryBase<GalaxyContainer>(_mongoClient);
                }
                return _galaxyContainerRepository;
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
        public IRepositoryBase<Ship> ShipRepository
        {
            get
            {
                if (_shipRepository == null)
                {
                    _shipRepository = new RepositoryBase<Ship>(_mongoClient);
                }
                return _shipRepository;
            }
        }

        public IRepositoryBase<ShipTemplate> ShipTemplateRepository
        {
            get
            {
                if (_shipTemplateRepository == null)
                {
                    _shipTemplateRepository = new RepositoryBase<ShipTemplate>(_mongoClient);
                }
                return _shipTemplateRepository;
            }
        }

        public IRepositoryBase<ShipModule> ShipModuleRepository
        {
            get
            {
                if (_shipModuleRepository == null)
                {
                    _shipModuleRepository = new RepositoryBase<ShipModule>(_mongoClient);
                }
                return _shipModuleRepository;
            }
        }

        public IRepositoryBase<ImplantTemplate> ImplantTemplateRepository
        {
            get
            {
                if (_implantTemplateRepository == null)
                {
                    _implantTemplateRepository = new RepositoryBase<ImplantTemplate>(_mongoClient);
                }
                return _implantTemplateRepository;
            }
        }

        public IRepositoryBase<MicroclusterTemplate> MicroclusterTemplateRepository
        {
            get
            {
                if (_microclusterTemplateRepository == null)
                {
                    _microclusterTemplateRepository = new RepositoryBase<MicroclusterTemplate>(_mongoClient);
                }
                return _microclusterTemplateRepository;
            }
        }

    }
}