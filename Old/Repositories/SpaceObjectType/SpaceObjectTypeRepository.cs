using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class SpaceObjectTypeRepository : RepositoryBase<SpaceObjectType>, ISpaceObjectTypeRepository
    {
        public SpaceObjectTypeRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
