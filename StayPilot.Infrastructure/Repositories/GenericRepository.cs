using Microsoft.EntityFrameworkCore;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Common;
using StayPilot.Infrastructure.Context;

namespace StayPilot.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StayPilotDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(StayPilotDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<T>> GetActiveListAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SetActiveStatusAsync(int id, bool isActive)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null)
            {
                return;
            }

            entity.IsActive = isActive;
            entity.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}