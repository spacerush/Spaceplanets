using SpaceRushEntities.Model;
using SpaceRushEntities.Views;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceStorageBalanceRepository : ViewRepositoryBase<ResourceStorageBalance>, IResourceStorageBalanceRepository
    {
        public ResourceStorageBalanceRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
