using Marten;
using MongoDB.Driver;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.Repositories.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IDocumentStore _databaseClient;

        private IRepositoryBase<GalaxyContainer> _galaxyContainerRepository;
        private IRepositoryBase<SpaceObject> _spaceObjectRepository;
        private IRepositoryBase<PlanetMetadata> _planetMetadataRepository;

        private IRepositoryBase<WebSession> _webSessionRepository;
        private IPlayerRepository _playerRepository;
        private IRepositoryBase<AccessToken> _accessTokenRepository;
        private IRepositoryBase<Character> _characterRepository;

        private IShipRepository _shipRepository;
        private IRepositoryBase<ShipTemplate> _shipTemplateRepository;
        private IRepositoryBase<ShipModule> _shipModuleRepository;

        #region character augmentation
        private IRepositoryBase<ImplantTemplate> _implantTemplateRepository;
        private IRepositoryBase<MicroclusterTemplate> _microclusterTemplateRepository;
        #endregion

        public RepositoryWrapper(IDocumentStore documentStore)
        {
            _databaseClient = documentStore;
        }

        public IRepositoryBase<GalaxyContainer> GalaxyContainerRepository
        {
            get
            {
                if (_galaxyContainerRepository == null)
                {
                    _galaxyContainerRepository = new RepositoryBase<GalaxyContainer>(_databaseClient);
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
                    _spaceObjectRepository = new RepositoryBase<SpaceObject>(_databaseClient);
                }
                return _spaceObjectRepository;
            }
        }

        public IRepositoryBase<PlanetMetadata> PlanetMetadataRepository
        {
            get
            {
                if (_planetMetadataRepository == null)
                {
                    _planetMetadataRepository = new RepositoryBase<PlanetMetadata>(_databaseClient);
                }
                return _planetMetadataRepository;
            }
        }


        public IRepositoryBase<WebSession> WebSessionRepository
        {
            get
            {
                if (_webSessionRepository == null)
                {
                    _webSessionRepository = new RepositoryBase<WebSession>(_databaseClient);
                }
                return _webSessionRepository;
            }
        }


        public IPlayerRepository PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    _playerRepository = new PlayerRepository(_databaseClient);
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
                    _accessTokenRepository = new RepositoryBase<AccessToken>(_databaseClient);
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
                    _characterRepository = new RepositoryBase<Character>(_databaseClient);
                }
                return _characterRepository;
            }
        }
        public IShipRepository ShipRepository
        {
            get
            {
                if (_shipRepository == null)
                {
                    _shipRepository = new ShipRepository(_databaseClient);
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
                    _shipTemplateRepository = new RepositoryBase<ShipTemplate>(_databaseClient);
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
                    _shipModuleRepository = new RepositoryBase<ShipModule>(_databaseClient);
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
                    _implantTemplateRepository = new RepositoryBase<ImplantTemplate>(_databaseClient);
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
                    _microclusterTemplateRepository = new RepositoryBase<MicroclusterTemplate>(_databaseClient);
                }
                return _microclusterTemplateRepository;
            }
        }

    }
}
