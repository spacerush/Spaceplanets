using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class StarSystemRepository : RepositoryBase<StarSystem>, IStarSystemRepository
    {
        public StarSystemRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
