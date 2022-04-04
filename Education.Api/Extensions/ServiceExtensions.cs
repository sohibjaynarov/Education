using Education.Data.IRepositories;
using Education.Data.Repositories;
using Education.Service.Interfaces;
using Education.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Education.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ICourseService, CourseService>();
        }
    }
}
