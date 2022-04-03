using AutoMapper;
using Education.Data.IRepositories;
using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Teachers;
using Education.Domain.Enums;
using Education.Service.DTOs.Teachers;
using Education.Service.Extentions;
using Education.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Teacher>> CreateAsync(TeacherForCreationDto teacherDto)
        {
            var response = new BaseResponse<Teacher>();

            // check for teacher
            var existTeacher = await unitOfWork.Teachers.GetAsync(p => p.Phone == teacherDto.Phone);
            if (existTeacher is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }

            // create after checking success
            var mappedTeacher = mapper.Map<Teacher>(teacherDto);

            // save image from dto model to wwwroot
            mappedTeacher.Image = await SaveFileAsync(teacherDto.Image.OpenReadStream(), teacherDto.Image.FileName);

            var result = await unitOfWork.Teachers.CreateAsync(mappedTeacher);

            result.Image = "https://localhost:5001/Images/" + result.Image;

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Teacher, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for teacher
            var existTeacher = await unitOfWork.Teachers.GetAsync(expression);
            if (existTeacher is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            existTeacher.Delete();

            var result = await unitOfWork.Teachers.UpdateAsync(existTeacher);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Teacher>>> GetAllAsync(PaginationParams @params, Expression<Func<Teacher, bool>> expression = null)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<Teacher>>();

            var students = await unitOfWork.Teachers.GetAllAsync(expression);

            response.Data = students.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Teacher>> GetAsync(Expression<Func<Teacher, bool>> expression)
        {
            var response = new BaseResponse<Teacher>();

            var teacher = await unitOfWork.Teachers.GetAsync(expression);
            if (teacher is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = teacher;

            return response;
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("Storage:ImageUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }

        public async Task<BaseResponse<Teacher>> UpdateAsync(Guid id, TeacherForCreationDto teacherDto)
        {
            var response = new BaseResponse<Teacher>();

            // check for exist student
            var teacher = await unitOfWork.Teachers.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (teacher is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            var mappedTeacher = mapper.Map<Teacher>(teacherDto);

            mappedTeacher.Update();

            var result = await unitOfWork.Teachers.UpdateAsync(mappedTeacher);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;

        }
    }
}
