using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class GroupAllPermissionRepository : ViewRepositoryBase<GroupAllPermission>, IGroupAllPermissionRepository
    {
        public GroupAllPermissionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
