using SpaceRushEntities.Model;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private GamedatabaseContext _context;
        private IApiRateLimitRepository _apiRateLimitRepository;
        private IApiUsageLogItemRepository _apiUsageLogItemRepository;
        private ICharacterRepository _characterRepository;
        private IClusterRepository _clusterRepository;
        private IClusterSlotRepository _clusterSlotRepository;
        private IComponentRepository _componentRepository;
        private IComponentSlotRepository _componentSlotRepository;
        private ICrewSlotRepository _crewSlotRepository;
        private IFactionRepository _factionRepository;
        private IFactionOpinionRepository _factionOpinionRepository;
        private IGenderRepository _genderRepository;
        private IGroupRepository _groupRepository;
        private IGroupMembershipRecordRepository _groupMembershipRecordRepository;
        private IGroupPermissionRepository _groupPermissionRepository;
        private IImplantRepository _implantRepository;
        private IImplantBaseItemRepository _implantBaseItemRepository;
        private IImplantClusterRepository _implantClusterRepository;
        private IImplantSlotRepository _implantSlotRepository;
        private IModuleRepository _moduleRepository;
        private IModuleBaseItemRepository _moduleBaseItemRepository;
        private IModuleComponentRepository _moduleComponentRepository;
        private IModuleSlotRepository _moduleSlotRepository;
        private IMotdItemRepository _motdItemRepository;
        private IPermissionRepository _permissionRepository;
        private IPlayerRepository _playerRepository;
        private IPlayerTokenRepository _playerTokenRepository;
        private IProfessionRepository _professionRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IResourceRepository _resourceRepository;
        private IResourceHubRepository _resourceHubRepository;
        private IResourceHubBalanceRepository _resourceHubBalanceRepository;
        private IResourceHubMiningEventRepository _resourceHubMiningEventRepository;
        private IResourceProcessorRepository _resourceProcessorRepository;
        private IResourceProcessorConversionEventRepository _resourceProcessorConversionEventRepository;
        private IResourceProcessorInputRepository _resourceProcessorInputRepository;
        private IResourceProcessorOutputRepository _resourceProcessorOutputRepository;
        private IResourceProcessorTypeRepository _resourceProcessorTypeRepository;
        private IResourceStorageRepository _resourceStorageRepository;
        private IResourceStorageBalanceRepository _resourceStorageBalanceRepository;
        private IResourceStorageEventRepository _resourceStorageEventRepository;
        private IResourceStorageTypeRepository _resourceStorageTypeRepository;
        private IShipRepository _shipRepository;
        private IShipAbilityRepository _shipAbilityRepository;
        private IShipCrewSlotRepository _shipCrewSlotRepository;
        private IShipTechnologyRepository _shipTechnologyRepository;
        private IShipTypeRepository _shipTypeRepository;
        private IShipTypeResourceRepository _shipTypeResourceRepository;
        private IShipTypesCrewSlotRepository _shipTypesCrewSlotRepository;
        private IShipyardRepository _shipyardRepository;
        private IShipyardShipTypeRepository _shipyardShipTypeRepository;
        private IShipyardTypeRepository _shipyardTypeRepository;
        private ISkillRepository _skillRepository;
        private ISpaceObjectRepository _spaceObjectRepository;
        private ISpaceObjectTypeRepository _spaceObjectTypeRepository;
        private IStarSystemRepository _starSystemRepository;

        #region Views
        //  Here are backing fields for some views related data.
        private IGroupActivePermissionRepository _groupActivePermissionRepository;
        private IStarSystemDistanceRepository _starSystemDistanceRepository;
        private IStarSystemSpaceObjectRepository _starSystemSpaceObjectRepository;
        private IClusterDescriptionRepository _clusterDescriptionRepository;
        private IApiUsageOverviewItemRepository _apiUsageOverviewItemRepository;
        private IGroupAllPermissionRepository _groupAllPermissionRepository;
        private IPlayerActivePermissionRepository _playerActivePermissionRepository;
        private IPlayerAllPermissionRepository _playerAllPermissionRepository;
        private IEquippedImplantRepository _equippedImplantRepository;

        #endregion

        private IRepositoryBase<StarSystem> _genericStarSystemRepository;
        public IRepositoryBase<StarSystem> GenericStarSystemRepository
        {
            get
            {
                if (_genericStarSystemRepository == null)
                {
                    _genericStarSystemRepository = new RepositoryBase<StarSystem>(_context);
                }
                return _genericStarSystemRepository;
            }
        }

        private IRepositoryBase<CharacterImplantInventoryItem> _characterImplantInventoryItemRepository;
        public IRepositoryBase<CharacterImplantInventoryItem> CharacterImplantInventoryItemRepository
        {
            get
            {
                if (_characterImplantInventoryItemRepository == null)
                {
                    _characterImplantInventoryItemRepository = new RepositoryBase<CharacterImplantInventoryItem>(_context);
                }
                return _characterImplantInventoryItemRepository;
            }
        }

        private IViewRepositoryBase<UnequippedImplant> _unequippedImplantRepository;
        public IViewRepositoryBase<UnequippedImplant> UnequippedImplantRepository
        {
            get
            {
                if (_unequippedImplantRepository == null)
                {
                    _unequippedImplantRepository = new ViewRepositoryBase<UnequippedImplant>(_context);
                }
                return _unequippedImplantRepository;
            }
        }


        public IApiRateLimitRepository ApiRateLimitRepository
        {
            get
            {
                if (_apiRateLimitRepository == null)
                {
                    _apiRateLimitRepository = new ApiRateLimitRepository(_context);
                }
                return _apiRateLimitRepository;
            }
        }
        public IApiUsageLogItemRepository ApiUsageLogItemRepository
        {
            get
            {
                if (_apiUsageLogItemRepository == null)
                {
                    _apiUsageLogItemRepository = new ApiUsageLogItemRepository(_context);
                }
                return _apiUsageLogItemRepository;
            }
        }
        public ICharacterRepository CharacterRepository
        {
            get
            {
                if (_characterRepository == null)
                {
                    _characterRepository = new CharacterRepository(_context);
                }
                return _characterRepository;
            }
        }
        public IClusterRepository ClusterRepository
        {
            get
            {
                if (_clusterRepository == null)
                {
                    _clusterRepository = new ClusterRepository(_context);
                }
                return _clusterRepository;
            }
        }
        public IComponentRepository ComponentRepository
        {
            get
            {
                if (_componentRepository == null)
                {
                    _componentRepository = new ComponentRepository(_context);
                }
                return _componentRepository;
            }
        }
        public IComponentSlotRepository ComponentSlotRepository
        {
            get
            {
                if (_componentSlotRepository == null)
                {
                    _componentSlotRepository = new ComponentSlotRepository(_context);
                }
                return _componentSlotRepository;
            }
        }
        public IClusterSlotRepository ClusterSlotRepository
        {
            get
            {
                if (_clusterSlotRepository == null)
                {
                    _clusterSlotRepository = new ClusterSlotRepository(_context);
                }
                return _clusterSlotRepository;
            }
        }
        public ICrewSlotRepository CrewSlotRepository
        {
            get
            {
                if (_crewSlotRepository == null)
                {
                    _crewSlotRepository = new CrewSlotRepository(_context);
                }
                return _crewSlotRepository;
            }
        }
        public IFactionRepository FactionRepository
        {
            get
            {
                if (_factionRepository == null)
                {
                    _factionRepository = new FactionRepository(_context);
                }
                return _factionRepository;
            }
        }
        public IFactionOpinionRepository FactionOpinionRepository
        {
            get
            {
                if (_factionOpinionRepository == null)
                {
                    _factionOpinionRepository = new FactionOpinionRepository(_context);
                }
                return _factionOpinionRepository;
            }
        }
        public IGenderRepository GenderRepository
        {
            get
            {
                if (_genderRepository == null)
                {
                    _genderRepository = new GenderRepository(_context);
                }
                return _genderRepository;
            }
        }
        public IGroupRepository GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GroupRepository(_context);
                }
                return _groupRepository;
            }
        }
        public IGroupMembershipRecordRepository GroupMembershipRecordRepository
        {
            get
            {
                if (_groupMembershipRecordRepository == null)
                {
                    _groupMembershipRecordRepository = new GroupMembershipRecordRepository(_context);
                }
                return _groupMembershipRecordRepository;
            }
        }
        public IGroupPermissionRepository GroupPermissionRepository
        {
            get
            {
                if (_groupPermissionRepository == null)
                {
                    _groupPermissionRepository = new GroupPermissionRepository(_context);
                }
                return _groupPermissionRepository;
            }
        }
        public IImplantRepository ImplantRepository
        {
            get
            {
                if (_implantRepository == null)
                {
                    _implantRepository = new ImplantRepository(_context);
                }
                return _implantRepository;
            }
        }
        public IImplantBaseItemRepository ImplantBaseItemRepository
        {
            get
            {
                if (_implantBaseItemRepository == null)
                {
                    _implantBaseItemRepository = new ImplantBaseItemRepository(_context);
                }
                return _implantBaseItemRepository;
            }
        }
        public IImplantClusterRepository ImplantClusterRepository
        {
            get
            {
                if (_implantClusterRepository == null)
                {
                    _implantClusterRepository = new ImplantClusterRepository(_context);
                }
                return _implantClusterRepository;
            }
        }
        public IImplantSlotRepository ImplantSlotRepository
        {
            get
            {
                if (_implantSlotRepository == null)
                {
                    _implantSlotRepository = new ImplantSlotRepository(_context);
                }
                return _implantSlotRepository;
            }
        }
        public IModuleRepository ModuleRepository
        {
            get
            {
                if (_moduleRepository == null)
                {
                    _moduleRepository = new ModuleRepository(_context);
                }
                return _moduleRepository;
            }
        }
        public IModuleBaseItemRepository ModuleBaseItemRepository
        {
            get
            {
                if (_moduleBaseItemRepository == null)
                {
                    _moduleBaseItemRepository = new ModuleBaseItemRepository(_context);
                }
                return _moduleBaseItemRepository;
            }
        }
        public IModuleComponentRepository ModuleComponentRepository
        {
            get
            {
                if (_moduleComponentRepository == null)
                {
                    _moduleComponentRepository = new ModuleComponentRepository(_context);
                }
                return _moduleComponentRepository;
            }
        }
        public IModuleSlotRepository ModuleSlotRepository
        {
            get
            {
                if (_moduleSlotRepository == null)
                {
                    _moduleSlotRepository = new ModuleSlotRepository(_context);
                }
                return _moduleSlotRepository;
            }
        }
        public IMotdItemRepository MotdItemRepository
        {
            get
            {
                if (_motdItemRepository == null)
                {
                    _motdItemRepository = new MotdItemRepository(_context);
                }
                return _motdItemRepository;
            }
        }
        public IPermissionRepository PermissionRepository
        {
            get
            {
                if (_permissionRepository == null)
                {
                    _permissionRepository = new PermissionRepository(_context);
                }
                return _permissionRepository;
            }
        }
        public IPlayerRepository PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    _playerRepository = new PlayerRepository(_context);
                }
                return _playerRepository;
            }
        }
        public IPlayerTokenRepository PlayerTokenRepository
        {
            get
            {
                if (_playerTokenRepository == null)
                {
                    _playerTokenRepository = new PlayerTokenRepository(_context);
                }
                return _playerTokenRepository;
            }
        }
        public IProfessionRepository ProfessionRepository
        {
            get
            {
                if (_professionRepository == null)
                {
                    _professionRepository = new ProfessionRepository(_context);
                }
                return _professionRepository;
            }
        }
        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if (_refreshTokenRepository == null)
                {
                    _refreshTokenRepository = new RefreshTokenRepository(_context);
                }
                return _refreshTokenRepository;
            }
        }
        public IResourceRepository ResourceRepository
        {
            get
            {
                if (_resourceRepository == null)
                {
                    _resourceRepository = new ResourceRepository(_context);
                }
                return _resourceRepository;
            }
        }
        public IResourceHubRepository ResourceHubRepository
        {
            get
            {
                if (_resourceHubRepository == null)
                {
                    _resourceHubRepository = new ResourceHubRepository(_context);
                }
                return _resourceHubRepository;
            }
        }
        public IResourceHubBalanceRepository ResourceHubBalanceRepository
        {
            get
            {
                if (_resourceHubBalanceRepository == null)
                {
                    _resourceHubBalanceRepository = new ResourceHubBalanceRepository(_context);
                }
                return _resourceHubBalanceRepository;
            }
        }
        public IResourceHubMiningEventRepository ResourceHubMiningEventRepository
        {
            get
            {
                if (_resourceHubMiningEventRepository == null)
                {
                    _resourceHubMiningEventRepository = new ResourceHubMiningEventRepository(_context);
                }
                return _resourceHubMiningEventRepository;
            }
        }
        public IResourceProcessorRepository ResourceProcessorRepository
        {
            get
            {
                if (_resourceProcessorRepository == null)
                {
                    _resourceProcessorRepository = new ResourceProcessorRepository(_context);
                }
                return _resourceProcessorRepository;
            }
        }
        public IResourceProcessorConversionEventRepository ResourceProcessorConversionEventRepository
        {
            get
            {
                if (_resourceProcessorConversionEventRepository == null)
                {
                    _resourceProcessorConversionEventRepository = new ResourceProcessorConversionEventRepository(_context);
                }
                return _resourceProcessorConversionEventRepository;
            }
        }
        public IResourceProcessorInputRepository ResourceProcessorInputRepository
        {
            get
            {
                if (_resourceProcessorInputRepository == null)
                {
                    _resourceProcessorInputRepository = new ResourceProcessorInputRepository(_context);
                }
                return _resourceProcessorInputRepository;
            }
        }
        public IResourceProcessorOutputRepository ResourceProcessorOutputRepository
        {
            get
            {
                if (_resourceProcessorOutputRepository == null)
                {
                    _resourceProcessorOutputRepository = new ResourceProcessorOutputRepository(_context);
                }
                return _resourceProcessorOutputRepository;
            }
        }
        public IResourceProcessorTypeRepository ResourceProcessorTypeRepository
        {
            get
            {
                if (_resourceProcessorTypeRepository == null)
                {
                    _resourceProcessorTypeRepository = new ResourceProcessorTypeRepository(_context);
                }
                return _resourceProcessorTypeRepository;
            }
        }
        public IResourceStorageRepository ResourceStorageRepository
        {
            get
            {
                if (_resourceStorageRepository == null)
                {
                    _resourceStorageRepository = new ResourceStorageRepository(_context);
                }
                return _resourceStorageRepository;
            }
        }
        public IResourceStorageBalanceRepository ResourceStorageBalanceRepository
        {
            get
            {
                if (_resourceStorageBalanceRepository == null)
                {
                    _resourceStorageBalanceRepository = new ResourceStorageBalanceRepository(_context);
                }
                return _resourceStorageBalanceRepository;
            }
        }
        public IResourceStorageEventRepository ResourceStorageEventRepository
        {
            get
            {
                if (_resourceStorageEventRepository == null)
                {
                    _resourceStorageEventRepository = new ResourceStorageEventRepository(_context);
                }
                return _resourceStorageEventRepository;
            }
        }
        public IResourceStorageTypeRepository ResourceStorageTypeRepository
        {
            get
            {
                if (_resourceStorageTypeRepository == null)
                {
                    _resourceStorageTypeRepository = new ResourceStorageTypeRepository(_context);
                }
                return _resourceStorageTypeRepository;
            }
        }
        public IShipRepository ShipRepository
        {
            get
            {
                if (_shipRepository == null)
                {
                    _shipRepository = new ShipRepository(_context);
                }
                return _shipRepository;
            }
        }
        public IShipAbilityRepository ShipAbilityRepository
        {
            get
            {
                if (_shipAbilityRepository == null)
                {
                    _shipAbilityRepository = new ShipAbilityRepository(_context);
                }
                return _shipAbilityRepository;
            }
        }
        public IShipCrewSlotRepository ShipCrewSlotRepository
        {
            get
            {
                if (_shipCrewSlotRepository == null)
                {
                    _shipCrewSlotRepository = new ShipCrewSlotRepository(_context);
                }
                return _shipCrewSlotRepository;
            }
        }
        public IShipTechnologyRepository ShipTechnologyRepository
        {
            get
            {
                if (_shipTechnologyRepository == null)
                {
                    _shipTechnologyRepository = new ShipTechnologyRepository(_context);
                }
                return _shipTechnologyRepository;
            }
        }
        public IShipTypeRepository ShipTypeRepository
        {
            get
            {
                if (_shipTypeRepository == null)
                {
                    _shipTypeRepository = new ShipTypeRepository(_context);
                }
                return _shipTypeRepository;
            }
        }
        public IShipTypeResourceRepository ShipTypeResourceRepository
        {
            get
            {
                if (_shipTypeResourceRepository == null)
                {
                    _shipTypeResourceRepository = new ShipTypeResourceRepository(_context);
                }
                return _shipTypeResourceRepository;
            }
        }
        public IShipyardRepository ShipyardRepository
        {
            get
            {
                if (_shipyardRepository == null)
                {
                    _shipyardRepository = new ShipyardRepository(_context);
                }
                return _shipyardRepository;
            }
        }
        public IShipyardShipTypeRepository ShipyardShipTypeRepository
        {
            get
            {
                if (_shipyardShipTypeRepository == null)
                {
                    _shipyardShipTypeRepository = new ShipyardShipTypeRepository(_context);
                }
                return _shipyardShipTypeRepository;
            }
        }
        public IShipyardTypeRepository ShipyardTypeRepository
        {
            get
            {
                if (_shipyardTypeRepository == null)
                {
                    _shipyardTypeRepository = new ShipyardTypeRepository(_context);
                }
                return _shipyardTypeRepository;
            }
        }
        public IShipTypesCrewSlotRepository ShipTypesCrewSlotRepository
        {
            get
            {
                if (_shipTypesCrewSlotRepository == null)
                {
                    _shipTypesCrewSlotRepository = new ShipTypesCrewSlotRepository(_context);
                }
                return _shipTypesCrewSlotRepository;
            }
        }
        public ISkillRepository SkillRepository
        {
            get
            {
                if (_skillRepository == null)
                {
                    _skillRepository = new SkillRepository(_context);
                }
                return _skillRepository;
            }
        }
        public ISpaceObjectRepository SpaceObjectRepository
        {
            get
            {
                if (_spaceObjectRepository == null)
                {
                    _spaceObjectRepository = new SpaceObjectRepository(_context);
                }
                return _spaceObjectRepository;
            }
        }
        public ISpaceObjectTypeRepository SpaceObjectTypeRepository
        {
            get
            {
                if (_spaceObjectTypeRepository == null)
                {
                    _spaceObjectTypeRepository = new SpaceObjectTypeRepository(_context);
                }
                return _spaceObjectTypeRepository;
            }
        }
        public IStarSystemRepository StarSystemRepository
        {
            get
            {
                if (_starSystemRepository == null)
                {
                    _starSystemRepository = new StarSystemRepository(_context);
                }
                return _starSystemRepository;
            }
        }


        /// <summary>
        /// Exposes clusters linked to the implant base items, with descriptions.
        /// </summary>
        public IClusterDescriptionRepository ClusterDescriptionRepository
        {
            get
            {
                if (_clusterDescriptionRepository == null)
                {
                    _clusterDescriptionRepository = new ClusterDescriptionRepository(_context);
                }
                return _clusterDescriptionRepository;
            }
        }

        /// <summary>
        /// Exposes every permission a group has or has ever had.
        /// </summary>
        public IGroupAllPermissionRepository GroupAllPermissionRepository
        {
            get
            {
                if (_groupAllPermissionRepository == null)
                {
                    _groupAllPermissionRepository = new GroupAllPermissionRepository(_context);
                }
                return _groupAllPermissionRepository;
            }
        }

        /// <summary>
        /// Exposes api usage.
        /// </summary>
        public IApiUsageOverviewItemRepository ApiUsageOverviewItemRepository
        {
            get
            {
                if (_apiUsageOverviewItemRepository == null)
                {
                    _apiUsageOverviewItemRepository = new ApiUsageOverviewItemRepository(_context);
                }
                return _apiUsageOverviewItemRepository;
            }
        }

        /// <summary>
        /// Exposes active permissions on a group.
        /// </summary>
        public IGroupActivePermissionRepository GroupActivePermissionRepository
        {
            get
            {
                if (_groupActivePermissionRepository == null)
                {
                    _groupActivePermissionRepository = new GroupActivePermissionRepository(_context);
                }
                return _groupActivePermissionRepository;
            }
        }

        /// <summary>
        /// Exposes every permission a player has or has ever had.
        /// </summary>
        public IPlayerAllPermissionRepository PlayerAllPermissionRepository
        {
            get
            {
                if (_playerAllPermissionRepository == null)
                {
                    _playerAllPermissionRepository = new PlayerAllPermissionRepository(_context);
                }
                return _playerAllPermissionRepository;
            }
        }

        /// <summary>
        /// Exposes active permissions on a player.
        /// </summary>
        public IPlayerActivePermissionRepository PlayerActivePermissionRepository
        {
            get
            {
                if (_playerActivePermissionRepository == null)
                {
                    _playerActivePermissionRepository = new PlayerActivePermissionRepository(_context);
                }
                return _playerActivePermissionRepository;
            }
        }

        /// <summary>
        /// Exposes the StarSystemDistances entities, representing distances from
        /// each star system to every other star system as calculated by the rdbms.
        /// </summary>
        public IStarSystemDistanceRepository StarSystemDistanceRepository
        {
            get
            {
                if (_starSystemDistanceRepository == null)
                {
                    _starSystemDistanceRepository = new StarSystemDistanceRepository(_context);
                }
                return _starSystemDistanceRepository;
            }
        }

        /// <summary>
        /// Exposes the StarSystemSpaceObjects entities, representing the objects within a given star system.
        /// </summary>
        public IStarSystemSpaceObjectRepository StarSystemSpaceObjectRepository
        {
            get
            {
                if (_starSystemSpaceObjectRepository == null)
                {
                    _starSystemSpaceObjectRepository = new StarSystemSpaceObjectRepository(_context);
                }
                return _starSystemSpaceObjectRepository;
            }
        }

        /// <summary>
        /// Exposes the EquippedImplants data (view) as a repository.
        /// </summary>
        public IEquippedImplantRepository EquippedImplantRepository
        {
            get
            {
                if (_equippedImplantRepository == null)
                {
                    _equippedImplantRepository = new EquippedImplantRepository(_context);
                }
                return _equippedImplantRepository;
            }
        }

        public RepositoryWrapper(GamedatabaseContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
