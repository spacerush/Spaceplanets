using SpaceRushEntities.Model;
using SpaceRushEntities.Views;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceHubBalanceRepository : ViewRepositoryBase<ResourceHubBalance>, IResourceHubBalanceRepository
    {
        public ResourceHubBalanceRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
