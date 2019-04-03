using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ClusterDescriptionRepository : ViewRepositoryBase<ClusterDescription>, IClusterDescriptionRepository
    {
        public ClusterDescriptionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
