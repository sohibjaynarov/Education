using Education.Data.Contexts;
using Education.Data.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EducationDbContext context;
        private readonly IConfiguration config;

        /// <summary>
        /// Repositories
        /// </summary>
        public IStudentRepository Students { get; private set; }

        public IGroupRepository Groups { get; private set; }

        public UnitOfWork(EducationDbContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;

            // Object initializing for repositories
            Students = new StudentRepository(context);
            Groups = new GroupRepository(context);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
