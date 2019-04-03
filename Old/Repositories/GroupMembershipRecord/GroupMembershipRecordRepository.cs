
using SpaceRushEntities.Model;
using SpaceRushEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRushEntities.Repositories
{
    public class GroupMembershipRecordRepository : RepositoryBase<GroupMembershipRecord>, IGroupMembershipRecordRepository
    {
        public GroupMembershipRecordRepository(GamedatabaseContext srContext)
            : base(srContext)
        {
        }
    }
}
