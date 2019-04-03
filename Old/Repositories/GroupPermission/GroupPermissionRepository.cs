
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class GroupPermissionRepository : RepositoryBase<GroupPermission>, IGroupPermissionRepository
    {
        public GroupPermissionRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
