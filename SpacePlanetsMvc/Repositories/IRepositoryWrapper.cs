using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePlanetsMvc.Repositories.Ships;

namespace SpacePlanetsMvc.Repositories
{
    public interface IRepositoryWrapper
    {
        #region Map
        IRepositoryBase<GalaxyContainer> GalaxyContainerRepository { get; }
        IRepositoryBase<SpaceObject> SpaceObjectRepository { get; }
        IRepositoryBase<PlanetMetadata> PlanetMetadataRepository { get; }
        #endregion

        #region Accounts
        IRepositoryBase<WebSession> WebSessionRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        IRepositoryBase<AccessToken> AccessTokenRepository { get; }
        #endregion

        #region OtherObjects
        IRepositoryBase<Character> CharacterRepository { get; }
        IShipRepository ShipRepository { get; }
        #endregion

        #region Defaults Or Templates
        IRepositoryBase<ShipTemplate> ShipTemplateRepository { get; }
        IRepositoryBase<ShipModule> ShipModuleRepository { get; }
        IRepositoryBase<ImplantTemplate> ImplantTemplateRepository { get; }
        IRepositoryBase<MicroclusterTemplate> MicroclusterTemplateRepository { get; }

        #endregion
    }
}
