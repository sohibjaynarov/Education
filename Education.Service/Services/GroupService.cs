using AutoMapper;
using Education.Data.IRepositories;
using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Groups;
using Education.Domain.Enums;
using Education.Service.DTOs.Groups;
using Education.Service.Extentions;
using Education.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Group>> CreateAsync(GroupForCreationDto groupDto)
        {
            var response = new BaseResponse<Group>();

            // check for Group
            var existGroup = await unitOfWork.Groups.GetAsync(p => p.Name == groupDto.Name);
            if (existGroup is not null)
            {
                response.Error = new ErrorResponse(400, "Group is exist");
                return response;
            }

            // check for teacher
            var existTeacher = await unitOfWork.Teachers.GetAsync(p => p.Id == groupDto.TeacherId);
            if (existTeacher is null)
            {
                response.Error = new ErrorResponse(404, "Teacher not found");
                return response;
            }

            // create after checking success
            var mappedGroup = mapper.Map<Group>(groupDto);

            var result = await unitOfWork.Groups.CreateAsync(mappedGroup);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Group, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist group
            var existGroup = await unitOfWork.Groups.GetAsync(expression);
            if (existGroup is null)
            {
                response.Error = new ErrorResponse(404, "Group not found");
                return response;
            }
            existGroup.Delete();

            var result = await unitOfWork.Groups.UpdateAsync(existGroup);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Group>>> GetAllAsync(PaginationParams @params, Expression<Func<Group, bool>> expression = null)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<Group>>();

            var groups = await unitOfWork.Groups.GetAllAsync(expression);

            response.Data = groups.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Group>> GetAsync(Expression<Func<Group, bool>> expression)
        {
            var response = new BaseResponse<Group>();

            var group = await unitOfWork.Groups.GetAsync(expression);
            if (group is null)
            {
                response.Error = new ErrorResponse(404, "Group not found");
                return response;
            }

            response.Data = group;

            return response;
        }

        public async Task<BaseResponse<Group>> UpdateAsync(Guid id, GroupForCreationDto groupDto)
        {
            var response = new BaseResponse<Group>();

            // check for exist group
            var group = await unitOfWork.Groups.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (group is null)
            {
                response.Error = new ErrorResponse(404, "Group not found");
                return response;
            }

            // check for exist teacher
            var teacher = await unitOfWork.Teachers.GetAsync(p => p.Id == groupDto.TeacherId);
            if (teacher is null)
            {
                response.Error = new ErrorResponse(404, "Teacher not found");
                return response;
            }


            var mappedGroup = mapper.Map<Group>(groupDto);

            mappedGroup.Update();

            var result = await unitOfWork.Groups.UpdateAsync(mappedGroup);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
