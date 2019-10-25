using Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly BudgetTrackerDbContext DbContext;
        protected readonly DbSet<TEntity> Set;

        public RepositoryBase(BudgetTrackerDbContext context)
        {
            DbContext = context;
            Set = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Set.Add(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Set.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(params object[] keyValues)
        {
            var entity = Set.Find(keyValues);

            if (entity == null)
            {
                throw new ArgumentException("Entity not found.");
            }

            return DeleteAsync(entity);
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return Set.FindAsync(keyValues);
        }

        public Task UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            return DbContext.SaveChangesAsync();
        }
    }
}
