using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Groups;
using Education.Service.DTOs.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Interfaces
{
    public interface IGroupService
    {
        Task<BaseResponse<Group>> CreateAsync(GroupForCreationDto groupDto);
        Task<BaseResponse<Group>> GetAsync(Expression<Func<Group, bool>> expression);
        Task<BaseResponse<IEnumerable<Group>>> GetAllAsync(PaginationParams @params, Expression<Func<Group, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Group, bool>> expression);
        Task<BaseResponse<Group>> UpdateAsync(Guid id, GroupForCreationDto groupDto);
    }
}
