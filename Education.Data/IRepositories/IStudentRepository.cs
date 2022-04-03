using Education.Domain.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.IRepositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
    }
}
