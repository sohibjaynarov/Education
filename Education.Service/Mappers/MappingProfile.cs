using AutoMapper;
using Education.Domain.Entities.Students;
using Education.Domain.Entities.Teachers;
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
        }
    }
}
