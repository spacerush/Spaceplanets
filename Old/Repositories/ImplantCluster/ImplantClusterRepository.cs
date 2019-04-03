
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ImplantClusterRepository : RepositoryBase<ImplantCluster>, IImplantClusterRepository
    {
        public ImplantClusterRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
