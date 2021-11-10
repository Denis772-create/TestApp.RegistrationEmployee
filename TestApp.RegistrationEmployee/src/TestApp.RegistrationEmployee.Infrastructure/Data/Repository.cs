using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApp.RegistrationEmployee.Core.Entities;
using TestApp.RegistrationEmployee.Core.Interfaces;

namespace TestApp.RegistrationEmployee.Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity<int>, IAggregateRoot
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public Task<TEntity> GetByIdAsync(int id) 
        {
            return _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<TEntity> GetByNameAsync(string name)
        {
            return _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Name == name);
        }

        public Task<List<TEntity>> ListAsync()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public async Task AddAsync(TEntity entity) 
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity) 
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity) 
        {
            _context.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities) 
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
