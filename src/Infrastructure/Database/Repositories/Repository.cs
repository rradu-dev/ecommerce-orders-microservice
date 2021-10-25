using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Pagination;
using Ecommerce.Services.Orders.Application.Repositories;
using Ecommerce.Services.Orders.Infrastructure.Database.Context;
using Ecommerce.Services.Orders.Infrastructure.Database.Pagination;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Repositories
{
    internal class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TId : struct, IEquatable<TId>
        where TEntity : class, IEntity<TId>, IAuditableEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetAsync(TId id)
            => await GetAsync(e => e.Id.Equals(id));

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>()
				.Where(predicate)
				.SingleOrDefaultAsync();

		public async Task<PageList<TEntity>> GetPageAsync(
			int page, int size, Expression<Func<TEntity, bool>> predicate = null)
		{
			var query = _context.Set<TEntity>()
				.AsNoTracking();

			if (predicate is null)
			{
				return await PageListBuilder
					.CreateAsync(query, page, size);
			}

			query = query.Where(predicate);
			return await PageListBuilder
				.CreateAsync(query, page, size);
		}

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

		public async Task UpdateAsync(TEntity entity)
        {
			_context.Set<TEntity>().Attach(entity);
			entity.UpdatedAt = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

		public async Task DeleteAsync(TId id)
        {
            var entity = await GetAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>()
				.AnyAsync(predicate);
    }
}
