using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipTypeResourceRepository : RepositoryBase<ShipTypesResource>, IShipTypeResourceRepository
    {
        public ShipTypeResourceRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
