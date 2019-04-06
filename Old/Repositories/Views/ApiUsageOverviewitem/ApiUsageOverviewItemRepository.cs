using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ApiUsageOverviewItemRepository : ViewRepositoryBase<ApiUsageOverviewItem>, IApiUsageOverviewItemRepository
    {
        public ApiUsageOverviewItemRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
