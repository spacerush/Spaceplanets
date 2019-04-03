
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ProfessionRepository : RepositoryBase<Profession>, IProfessionRepository
    {
        public ProfessionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
