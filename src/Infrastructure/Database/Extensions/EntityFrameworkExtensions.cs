using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Extensions
{
	internal static class EntityFrameworkExtensions
	{
		internal static void ConfigureDbContextOptions(
				this DbContextOptionsBuilder options,
				string connectionString)
        {
            var migrationAssemblyName = typeof(EntityFrameworkExtensions)
				.Assembly
				.GetName()
				.Name;

            options.UseNpgsql(connectionString, b =>
					b.MigrationsAssembly(migrationAssemblyName));
		}
	}
}
