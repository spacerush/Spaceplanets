
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipTypesCrewSlotRepository : RepositoryBase<ShipTypesCrewSlot>, IShipTypesCrewSlotRepository
    {
        public ShipTypesCrewSlotRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
