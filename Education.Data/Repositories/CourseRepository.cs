using Education.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Courses;
using Education.Data.Contexts;

namespace Education.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(EducationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
