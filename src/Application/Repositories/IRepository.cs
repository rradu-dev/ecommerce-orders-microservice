using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Pagination;

namespace Ecommerce.Services.Orders.Application.Repositories
{
    public interface IRepository<TEntity, TId>
        where TId : struct, IEquatable<TId>
        where TEntity : class, IEntity<TId>, IAuditableEntity
    {
        Task<TEntity> GetAsync(TId id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
		Task<PageList<TEntity>> GetPageAsync(int page, int size,
			Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(TId id);
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
