using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Groups;
using Education.Data.IRepositories;
using Education.Data.Contexts;

namespace Education.Data.Repositories
{
    internal class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(EducationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
