using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipyardRepository : RepositoryBase<Shipyard>, IShipyardRepository
    {
        public ShipyardRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
