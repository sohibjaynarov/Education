using AutoMapper;
using Education.Data.IRepositories;
using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Courses;
using Education.Domain.Enums;
using Education.Service.DTOs.Courses;
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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto courseDto)
        {
            var response = new BaseResponse<Course>();

            // check for Course
            var existCourse = await unitOfWork.Courses.GetAsync(p => p.Name == courseDto.Name);
            if (existCourse is not null)
            {
                response.Error = new ErrorResponse(400, "Course is exist");
                return response;
            }

            // check for teacher
            var existTeacher = await unitOfWork.Teachers.GetAsync(p => p.Id == courseDto.TeacherId);
            if (existTeacher is null)
            {
                response.Error = new ErrorResponse(404, "Teacher not found");
                return response;
            }

            // create after checking success
            var mappedCourse = mapper.Map<Course>(courseDto);

            // save video from dto model to wwwroot
            mappedCourse.Video = await SaveFileAsync(courseDto.Video.OpenReadStream(), courseDto.Video.FileName);

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);

            result.Video = config.GetSection("FileUrl:VideoUrl").Value + result.Video;


            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist course
            var existCourse = await unitOfWork.Courses.GetAsync(expression);
            if (existCourse is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }
            existCourse.Delete();

            var result = await unitOfWork.Courses.UpdateAsync(existCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<Course>>();

            var courses = await unitOfWork.Courses.GetAllAsync(expression);

            response.Data = courses.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(expression);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseForCreationDto courseDto)
        {
            var response = new BaseResponse<Course>();

            // check for exist group
            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            // check for exist teacher
            var teacher = await unitOfWork.Teachers.GetAsync(p => p.Id == courseDto.TeacherId);
            if (teacher is null)
            {
                response.Error = new ErrorResponse(404, "Teacher not found");
                return response;
            }


            var mappedCourse = mapper.Map<Course>(courseDto);

            mappedCourse.Update();

            var result = await unitOfWork.Courses.UpdateAsync(mappedCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("Storage:VideoUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }
    }
}
