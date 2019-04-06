using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class SpaceObjectRepository : RepositoryBase<SpaceObject>, ISpaceObjectRepository
    {
        public SpaceObjectRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
