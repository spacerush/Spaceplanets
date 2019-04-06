using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceStorageTypeRepository : RepositoryBase<ResourceStorageType>, IResourceStorageTypeRepository
    {
        public ResourceStorageTypeRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
