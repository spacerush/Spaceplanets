using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceProcessorConversionEventRepository : RepositoryBase<ResourceProcessorConversionEvent>, IResourceProcessorConversionEventRepository
    {
        public ResourceProcessorConversionEventRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
