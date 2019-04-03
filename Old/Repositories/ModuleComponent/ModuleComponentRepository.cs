
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ModuleComponentRepository : RepositoryBase<ModuleComponent>, IModuleComponentRepository
    {
        public ModuleComponentRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
