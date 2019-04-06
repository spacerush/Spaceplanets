
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class ApiRateLimitRepository : RepositoryBase<ApiRateLimit>, IApiRateLimitRepository
    {
        public ApiRateLimitRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
