
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
    {
        public SkillRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
