using Education.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.IRepositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
    }
}
