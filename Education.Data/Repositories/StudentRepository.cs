using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Students;
using Education.Data.IRepositories;
using Education.Data.Contexts;

namespace Education.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(EducationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
