using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(params object[] keyValues);
    }
}
