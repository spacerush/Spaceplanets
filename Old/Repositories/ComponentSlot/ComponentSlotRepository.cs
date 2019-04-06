using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ComponentSlotRepository : RepositoryBase<ComponentSlot>, IComponentSlotRepository
    {
        public ComponentSlotRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
