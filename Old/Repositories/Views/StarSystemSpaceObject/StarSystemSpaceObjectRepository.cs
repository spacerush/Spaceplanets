using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class StarSystemSpaceObjectRepository : ViewRepositoryBase<StarSystemSpaceObject>, IStarSystemSpaceObjectRepository
    {
        public StarSystemSpaceObjectRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
