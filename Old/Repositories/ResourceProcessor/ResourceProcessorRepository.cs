using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceProcessorRepository : RepositoryBase<ResourceProcessor>, IResourceProcessorRepository
    {
        public ResourceProcessorRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
