using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class MotdItemRepository : RepositoryBase<MotdItem>, IMotdItemRepository
    {
        public MotdItemRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
