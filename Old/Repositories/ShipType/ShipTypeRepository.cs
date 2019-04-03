using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipTypeRepository : RepositoryBase<ShipType>, IShipTypeRepository
    {
        public ShipTypeRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
