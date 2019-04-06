using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceProcessorOutputRepository : RepositoryBase<ResourceProcessorOutput>, IResourceProcessorOutputRepository
    {
        public ResourceProcessorOutputRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
