using Education.Domain.Entities.Courses;
using Education.Domain.Entities.Groups;
using Education.Domain.Entities.Students;
using Education.Domain.Entities.Teachers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.Contexts
{
    public class EducationDbContext : DbContext
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
    }
}
