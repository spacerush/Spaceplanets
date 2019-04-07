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
        IRepositoryBase<Player> PlayerRepository { get; }
        IRepositoryBase<AccessToken> AccessTokenRepository { get; }
        #endregion

    }
}
