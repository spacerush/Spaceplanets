using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.Repositories
{
    public interface IRepositoryWrapper
    {
        #region Map
        IRepositoryBase<Galaxy> GalaxyRepository { get; }
        IRepositoryBase<StarSystem> StarSystemRepository { get; }
        IRepositoryBase<SpaceObject> SpaceObjectRepository { get; }
        #endregion

        #region Accounts
        IRepositoryBase<WebSession> WebSessionRepository { get; }
        IRepositoryBase<Player> PlayerRepository { get; }
        IRepositoryBase<AccessToken> AccessTokenRepository { get; }
        #endregion

        #region OtherObjects
        IRepositoryBase<Character> CharacterRepository { get; }
        IRepositoryBase<Ship> ShipRepository { get; }
        #endregion

        #region Defaults Or Templates
        IRepositoryBase<ShipTemplate> ShipTemplateRepository { get; }
        IRepositoryBase<ShipModule> ShipModuleRepository { get; }
        #endregion
    }
}
