using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class PlayerTokenRepository : RepositoryBase<PlayerToken>, IPlayerTokenRepository
    {
        public PlayerTokenRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
