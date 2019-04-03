using SpaceRushEntities.Model;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public interface IRepositoryWrapper
    {
        IApiRateLimitRepository ApiRateLimitRepository { get; }
        IApiUsageLogItemRepository ApiUsageLogItemRepository { get; }
        ICharacterRepository CharacterRepository { get; }
        IClusterRepository ClusterRepository { get; }
        IClusterSlotRepository ClusterSlotRepository { get; }
        IComponentRepository ComponentRepository { get; }
        IComponentSlotRepository ComponentSlotRepository { get; }
        ICrewSlotRepository CrewSlotRepository { get; }
        IFactionRepository FactionRepository { get; }
        IFactionOpinionRepository FactionOpinionRepository { get; }
        IGenderRepository GenderRepository { get; }
        IGroupRepository GroupRepository { get; }
        IGroupMembershipRecordRepository GroupMembershipRecordRepository { get; }
        IGroupPermissionRepository GroupPermissionRepository { get; }
        IImplantRepository ImplantRepository { get; }
        IImplantBaseItemRepository ImplantBaseItemRepository { get; }
        IImplantClusterRepository ImplantClusterRepository { get; }
        IImplantSlotRepository ImplantSlotRepository { get; }
        IModuleRepository ModuleRepository { get; }
        IModuleBaseItemRepository ModuleBaseItemRepository { get; }
        IModuleComponentRepository ModuleComponentRepository { get; }
        IModuleSlotRepository ModuleSlotRepository { get; }
        IMotdItemRepository MotdItemRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        IPlayerTokenRepository PlayerTokenRepository { get; }
        IProfessionRepository ProfessionRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IResourceRepository ResourceRepository { get; }
        IResourceHubRepository ResourceHubRepository { get; }
        IResourceHubBalanceRepository ResourceHubBalanceRepository { get; }
        IResourceHubMiningEventRepository ResourceHubMiningEventRepository { get; }
        IResourceProcessorRepository ResourceProcessorRepository { get; }
        IResourceProcessorConversionEventRepository ResourceProcessorConversionEventRepository { get; }
        IResourceProcessorInputRepository ResourceProcessorInputRepository { get; }
        IResourceProcessorOutputRepository ResourceProcessorOutputRepository { get; }
        IResourceProcessorTypeRepository ResourceProcessorTypeRepository { get; }
        IResourceStorageRepository ResourceStorageRepository { get; }
        IResourceStorageBalanceRepository ResourceStorageBalanceRepository { get; }
        IResourceStorageEventRepository ResourceStorageEventRepository { get; }
        IResourceStorageTypeRepository ResourceStorageTypeRepository { get; }
        IShipRepository ShipRepository { get; }
        IShipAbilityRepository ShipAbilityRepository { get; }
        IShipCrewSlotRepository ShipCrewSlotRepository { get; }
        IShipTechnologyRepository ShipTechnologyRepository { get; }
        IShipTypeRepository ShipTypeRepository { get; }
        IShipTypeResourceRepository ShipTypeResourceRepository { get; }
        IShipTypesCrewSlotRepository ShipTypesCrewSlotRepository { get; }
        IShipyardRepository ShipyardRepository { get; }
        IShipyardShipTypeRepository ShipyardShipTypeRepository { get; }
        IShipyardTypeRepository ShipyardTypeRepository { get; }
        ISkillRepository SkillRepository { get; }
        ISpaceObjectRepository SpaceObjectRepository { get; }
        ISpaceObjectTypeRepository SpaceObjectTypeRepository { get; }
        IStarSystemRepository StarSystemRepository { get; }

        // Views below, that's why they are out of order.
        IStarSystemDistanceRepository StarSystemDistanceRepository { get; }
        IStarSystemSpaceObjectRepository StarSystemSpaceObjectRepository { get; }
        IApiUsageOverviewItemRepository ApiUsageOverviewItemRepository { get; }
        IClusterDescriptionRepository ClusterDescriptionRepository { get; }
        IGroupActivePermissionRepository GroupActivePermissionRepository { get; }
        IGroupAllPermissionRepository GroupAllPermissionRepository { get; }
        IPlayerActivePermissionRepository PlayerActivePermissionRepository { get; }
        IPlayerAllPermissionRepository PlayerAllPermissionRepository { get; }
        IEquippedImplantRepository EquippedImplantRepository { get; }

        // Special:
        IRepositoryBase<StarSystem> GenericStarSystemRepository { get; }
        IRepositoryBase<CharacterImplantInventoryItem> CharacterImplantInventoryItemRepository { get; }
        // Special views
        IViewRepositoryBase<UnequippedImplant> UnequippedImplantRepository { get; }
        /// <summary>
        /// Save changes to the underlying context.
        /// </summary>
        /// <returns>Number of affected records.</returns>
        int Save();
    }
}
