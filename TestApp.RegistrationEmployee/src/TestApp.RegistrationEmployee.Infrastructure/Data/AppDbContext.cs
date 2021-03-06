using Microsoft.EntityFrameworkCore;
using TestApp.RegistrationEmployee.Core.Entities;

namespace TestApp.RegistrationEmployee.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}