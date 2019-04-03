using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceStorageEventRepository : RepositoryBase<ResourceStorageEvent>, IResourceStorageEventRepository
    {
        public ResourceStorageEventRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
