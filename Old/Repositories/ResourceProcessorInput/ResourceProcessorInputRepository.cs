using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceProcessorInputRepository : RepositoryBase<ResourceProcessorInput>, IResourceProcessorInputRepository
    {
        public ResourceProcessorInputRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
