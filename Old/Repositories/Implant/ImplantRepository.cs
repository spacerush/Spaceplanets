
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ImplantRepository : RepositoryBase<Implant>, IImplantRepository
    {
        public ImplantRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
