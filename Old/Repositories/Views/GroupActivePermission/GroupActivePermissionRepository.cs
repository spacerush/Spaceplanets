using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using SpaceRushEntities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class GroupActivePermissionRepository : ViewRepositoryBase<GroupActivePermission>, IGroupActivePermissionRepository
    {
        public GroupActivePermissionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
