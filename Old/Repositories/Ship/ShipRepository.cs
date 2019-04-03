using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipRepository : RepositoryBase<Ship>, IShipRepository
    {
        public ShipRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
