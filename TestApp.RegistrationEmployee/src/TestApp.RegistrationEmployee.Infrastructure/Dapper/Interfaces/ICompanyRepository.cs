using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.RegistrationEmployee.Core.Entities;

namespace TestApp.RegistrationEmployee.Infrastructure.Dapper.Interfaces
{
    public interface ICompanyRepository
    {
        void CreateAsync(Company company);
        void DeleteAsync(int id);
        Company GetById(int id);
        Task<IEnumerable<Company>> GetAlListAsync();
        void UpdateAsync(Company company);
    }
}
