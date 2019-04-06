using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class PlayerAllPermissionRepository : ViewRepositoryBase<PlayerAllPermission>, IPlayerAllPermissionRepository
    {
        public PlayerAllPermissionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
