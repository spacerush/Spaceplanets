using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.Repositories
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<Galaxy> GalaxyRepository { get; }
        IRepositoryBase<Player> PlayerRepository { get; }

        IRepositoryBase<AccessToken> AccessTokenRepository { get; }

    }
}
