
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ImplantSlotRepository : RepositoryBase<ImplantSlot>, IImplantSlotRepository
    {
        public ImplantSlotRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
