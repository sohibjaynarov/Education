using AutoMapper;
using Education.Domain.Entities.Courses;
using Education.Domain.Entities.Groups;
using Education.Domain.Entities.Students;
using Education.Domain.Entities.Teachers;
using Education.Service.DTOs.Courses;
using Education.Service.DTOs.Groups;
using Education.Service.DTOs.Students;
using Education.Service.DTOs.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentForCreationDto, Student>().ReverseMap();
            CreateMap<TeacherForCreationDto, Teacher>().ReverseMap();
            CreateMap<GroupForCreationDto, Group>().ReverseMap();
            CreateMap<CourseForCreationDto, Course>().ReverseMap();
        }
    }
}
