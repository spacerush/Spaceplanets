using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipyardTypeRepository : RepositoryBase<ShipyardType>, IShipyardTypeRepository
    {
        public ShipyardTypeRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
