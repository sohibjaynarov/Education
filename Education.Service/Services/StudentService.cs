﻿using AutoMapper;
using Education.Data.IRepositories;
using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Students;
using Education.Domain.Enums;
using Education.Service.DTOs.Students;
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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Student>> CreateAsync(StudentForCreationDto studentDto)
        {
            var response = new BaseResponse<Student>();

            // check for student
            var existStudent = await unitOfWork.Students.GetAsync(p => p.Phone == studentDto.Phone);
            if (existStudent is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }

            // check for group
            var existGroup = await unitOfWork.Courses.GetAsync(p => p.Id == studentDto.GroupId);
            if (existGroup is null)
            {
                response.Error = new ErrorResponse(404, "Group not found");
                return response;
            }

            // create after checking success
            var mappedStudent = mapper.Map<Student>(studentDto);

            // save image from dto model to wwwroot
            mappedStudent.Image = await SaveFileAsync(studentDto.Image.OpenReadStream(), studentDto.Image.FileName);

            var result = await unitOfWork.Students.CreateAsync(mappedStudent);

            result.Image = "https://localhost:5001/Images/" + result.Image;

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Student, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist student
            var existStudent = await unitOfWork.Students.GetAsync(expression);
            if (existStudent is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }
            existStudent.Delete();

            var result = await unitOfWork.Students.UpdateAsync(existStudent);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Student>>> GetAllAsync(PaginationParams @params, Expression<Func<Student, bool>> expression = null)
        {


            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<Student>>();

            var students = await unitOfWork.Students.GetAllAsync(expression);

            response.Data = students.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Student>> GetAsync(Expression<Func<Student, bool>> expression)
        {
            var response = new BaseResponse<Student>();

            var student = await unitOfWork.Students.GetAsync(expression);
            if (student is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = student;

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

        public async Task<BaseResponse<Student>> UpdateAsync(Guid id, StudentForCreationDto studentDto)
        {
            var response = new BaseResponse<Student>();

            // check for exist student
            var student = await unitOfWork.Students.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (student is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            // check for exist group
            var group = await unitOfWork.Courses.GetAsync(p => p.Id == studentDto.GroupId);
            if (group is null)
            {
                response.Error = new ErrorResponse(404, "Group not found");
                return response;
            }

            var mappedStudent = mapper.Map<Student>(studentDto);

            mappedStudent.Update();

            var result = await unitOfWork.Students.UpdateAsync(mappedStudent);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
