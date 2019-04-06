using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class FactionOpinionRepository : RepositoryBase<FactionOpinion>, IFactionOpinionRepository
    {
        public FactionOpinionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
