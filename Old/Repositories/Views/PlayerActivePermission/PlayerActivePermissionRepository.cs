using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class PlayerActivePermissionRepository : ViewRepositoryBase<PlayerActivePermission>, IPlayerActivePermissionRepository
    {
        public PlayerActivePermissionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
