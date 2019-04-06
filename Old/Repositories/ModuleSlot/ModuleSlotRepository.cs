
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ModuleSlotRepository : RepositoryBase<ModuleSlot>, IModuleSlotRepository
    {
        public ModuleSlotRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
