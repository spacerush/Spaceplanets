
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ImplantBaseItemRepository : RepositoryBase<ImplantBaseItem>, IImplantBaseItemRepository
    {
        public ImplantBaseItemRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
