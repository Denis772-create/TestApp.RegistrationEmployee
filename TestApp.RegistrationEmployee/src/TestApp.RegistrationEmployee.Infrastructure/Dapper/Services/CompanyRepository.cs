using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using TestApp.RegistrationEmployee.Core.Entities;
using TestApp.RegistrationEmployee.Infrastructure.Dapper.Interfaces;

namespace TestApp.RegistrationEmployee.Infrastructure.Dapper.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbConnection _dbConnection;
        public CompanyRepository(string conn)
        {
            _dbConnection = new SqlConnection(conn);
        }

        public async void CreateAsync(Company company)
        {
            var sqlQuery = "INSERT INTO Companies (Name, OrganizationalAndLegalForm) VALUES(@Name, @OrganizationalAndLegalForm)";
            await _dbConnection.ExecuteAsync(sqlQuery, company);
        }

        public async void DeleteAsync(int id)
        {
            var sqlQuery = "DELETE FROM Companies WHERE Id = @id";
            await _dbConnection.ExecuteAsync(sqlQuery, new { id });
        }

        public Company GetById(int id)
        {
            return _dbConnection.Query<Company>("SELECT * FROM Companies WHERE Id = @id", new { id }).FirstOrDefault();
        }

        public async Task<IEnumerable<Company>> GetAlListAsync()
        {
            return await _dbConnection.QueryAsync<Company>("SELECT * FROM Companies");
        }

        public async void UpdateAsync(Company company)
        {
            var sqlQuery = "UPDATE Users SET Name = @Name, OrganizationalAndLegalForm = @OrganizationalAndLegalForm WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sqlQuery, company);
        }
    }
}
