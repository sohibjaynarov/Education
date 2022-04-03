using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Students;
using Education.Service.DTOs.Students;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Interfaces
{
    public interface IStudentService
    {
        Task<BaseResponse<Student>> CreateAsync(StudentForCreationDto studentDto);
        Task<BaseResponse<Student>> GetAsync(Expression<Func<Student, bool>> expression);
        Task<BaseResponse<IEnumerable<Student>>> GetAllAsync(PaginationParams @params, Expression<Func<Student, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Student, bool>> expression);
        Task<BaseResponse<Student>> UpdateAsync(Guid id, StudentForCreationDto studentDto);

        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}
