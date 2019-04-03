using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ComponentRepository : RepositoryBase<Component>, IComponentRepository
    {
        public ComponentRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
