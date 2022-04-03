using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Teachers;
using Education.Data.IRepositories;
using Education.Data.Contexts;

namespace Education.Data.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(EducationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
