using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class FactionRepository : RepositoryBase<Faction>, IFactionRepository
    {
        public FactionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
