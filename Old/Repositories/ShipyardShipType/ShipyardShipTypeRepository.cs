using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipyardShipTypeRepository : RepositoryBase<ShipyardShipType>, IShipyardShipTypeRepository
    {
        public ShipyardShipTypeRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
