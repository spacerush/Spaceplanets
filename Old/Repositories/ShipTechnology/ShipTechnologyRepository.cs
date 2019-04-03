
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipTechnologyRepository : RepositoryBase<ShipTechnology>, IShipTechnologyRepository
    {
        public ShipTechnologyRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
