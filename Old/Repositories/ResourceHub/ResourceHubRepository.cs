using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceHubRepository : RepositoryBase<ResourceHub>, IResourceHubRepository
    {
        public ResourceHubRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
