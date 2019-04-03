using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceProcessorTypeRepository : RepositoryBase<ResourceProcessorType>, IResourceProcessorTypeRepository
    {
        public ResourceProcessorTypeRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
