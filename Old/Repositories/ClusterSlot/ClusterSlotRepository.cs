
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ClusterSlotRepository : RepositoryBase<ClusterSlot>, IClusterSlotRepository
    {
        public ClusterSlotRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
