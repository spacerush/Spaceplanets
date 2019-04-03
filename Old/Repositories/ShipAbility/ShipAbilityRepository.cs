
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ShipAbilityRepository : RepositoryBase<ShipAbility>, IShipAbilityRepository
    {
        public ShipAbilityRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
