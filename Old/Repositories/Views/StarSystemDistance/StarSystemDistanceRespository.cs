using SpaceRushEntities.Model;
using SpaceRushEntities.Views;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class StarSystemDistanceRepository : ViewRepositoryBase<StarSystemDistance>, IStarSystemDistanceRepository
    {
        public StarSystemDistanceRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
