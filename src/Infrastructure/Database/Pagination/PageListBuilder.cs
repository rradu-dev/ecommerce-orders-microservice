using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Core.Pagination;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Pagination
{
	internal static class PageListBuilder
	{
		public static async Task<PageList<T>> CreateAsync<T>(
			IQueryable<T> source, int page, int size)
			where T : IAuditableEntity
		{
			var count = await source.CountAsync();
			var items = await source
				.OrderBy(s => s.CreatedAt)
				.Skip((page - 1) * size)
				.Take(size)
				.ToListAsync();

			return new PageList<T>(items, count, page, size);
		}
	}
}
