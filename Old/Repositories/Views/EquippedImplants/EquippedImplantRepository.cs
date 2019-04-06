using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class EquippedImplantRepository : ViewRepositoryBase<EquippedImplant>, IEquippedImplantRepository
    {
        public EquippedImplantRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
