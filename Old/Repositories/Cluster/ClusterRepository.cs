
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ClusterRepository : RepositoryBase<Cluster>, IClusterRepository
    {
        public ClusterRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
