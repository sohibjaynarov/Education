using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Teachers;
using Education.Service.DTOs.Teachers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Interfaces
{
    public interface ITeacherService
    {

        Task<BaseResponse<Teacher>> CreateAsync(TeacherForCreationDto teacherDto);
        Task<BaseResponse<Teacher>> GetAsync(Expression<Func<Teacher, bool>> expression);
        Task<BaseResponse<IEnumerable<Teacher>>> GetAllAsync(PaginationParams @params, Expression<Func<Teacher, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Teacher, bool>> expression);
        Task<BaseResponse<Teacher>> UpdateAsync(Guid id, TeacherForCreationDto teacherDto);

        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}
