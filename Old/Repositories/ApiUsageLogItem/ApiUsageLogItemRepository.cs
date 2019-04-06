
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ApiUsageLogItemRepository : RepositoryBase<ApiUsageLogItem>, IApiUsageLogItemRepository
    {
        public ApiUsageLogItemRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
