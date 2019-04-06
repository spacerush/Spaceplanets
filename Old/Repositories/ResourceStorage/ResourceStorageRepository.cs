using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceStorageRepository : RepositoryBase<ResourceStorage>, IResourceStorageRepository
    {
        public ResourceStorageRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
