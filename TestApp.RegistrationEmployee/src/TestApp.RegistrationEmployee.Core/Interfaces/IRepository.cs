using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestApp.RegistrationEmployee.Core.Entities;

namespace TestApp.RegistrationEmployee.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByNameAsync(string name);
        Task<List<TEntity>> ListAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task SaveChangesAsync();

    }
}
