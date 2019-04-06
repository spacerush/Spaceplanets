
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class CrewSlotRepository : RepositoryBase<CrewSlot>, ICrewSlotRepository
    {
        public CrewSlotRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
