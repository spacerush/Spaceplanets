
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipCrewSlotRepository : RepositoryBase<ShipCrewSlot>, IShipCrewSlotRepository
    {
        public ShipCrewSlotRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
