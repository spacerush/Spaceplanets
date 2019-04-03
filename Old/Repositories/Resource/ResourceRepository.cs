using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ResourceRepository : RepositoryBase<Resource>, IResourceRepository
    {
        public ResourceRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
