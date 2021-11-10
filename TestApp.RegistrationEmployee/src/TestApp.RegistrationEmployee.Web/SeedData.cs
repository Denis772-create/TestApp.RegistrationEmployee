using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestApp.RegistrationEmployee.Core.Entities;
using TestApp.RegistrationEmployee.Infrastructure.Data;

namespace TestApp.RegistrationEmployee.Web
{
    public static class SeedData
    {
        public static readonly Company Company1 = new Company
        {
            Name = "EPAM",
            OrganizationalAndLegalForm = "OAO"
        };
        public static readonly Company Company2 = new Company
        {
            Name = "Microsoft",
            OrganizationalAndLegalForm = "CAO"
        };
        public static readonly Company Company3 = new Company
        {
            Name = "Apple",
            OrganizationalAndLegalForm = "OAO"
        };

        public static readonly Employee Employee1 = new Employee
        {
            Name = "Tom",
            SecondName = "Treioa",
            LastName = "Moanrat",
            EmploymentDate = DateTime.Now,
            Position = "Senior",
            Company = Company1
        };
        public static readonly Employee Employee2 = new Employee
        {
            Name = "Bob",
            SecondName = "Treioa",
            LastName = "Ewepoanrt",
            EmploymentDate = DateTime.Now,
            Position = "Junior",
            Company = Company2
        };
        public static readonly Employee Employee3 = new Employee
        {
            Name = "Rick",
            SecondName = "Treioa",
            LastName = "Redpoanrt",
            EmploymentDate = DateTime.Now,
            Position = "Senior",
            Company = Company1
        };


        public static async void Initialize(IServiceProvider serviceProvider)
        {
            await using var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (dbContext.Companies.Any() || dbContext.Employees.Any())
                return;

            await PopulateStartTestData(dbContext);
        }
        public static async Task PopulateStartTestData(AppDbContext dbContext)
        {
            await dbContext.Companies.AddRangeAsync(Company1, Company2, Company3);
            await dbContext.Employees.AddRangeAsync(Employee1, Employee2, Employee3);
            dbContext.SaveChanges();
        }
    }
}
