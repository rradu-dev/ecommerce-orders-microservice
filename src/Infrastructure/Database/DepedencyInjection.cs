using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Application.Repositories;
using Ecommerce.Services.Orders.Infrastructure.Database.Context;
using Ecommerce.Services.Orders.Infrastructure.Database.Repositories;

namespace Ecommerce.Services.Orders.Infrastructure.Database
{
    public static class DependencyInjection
    {
        internal static IServiceCollection AddDatabase(
            this IServiceCollection services,
			string connectionString)
        {
            var migrationAssemblyName = typeof(DependencyInjection)
                .Assembly
                .GetName()
                .Name;

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
					b => b.MigrationsAssembly(migrationAssemblyName));
            }, ServiceLifetime.Scoped);

            return services;
        }

        internal static void Migrate(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider
					.GetService<AppDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        internal static void AddRepository<TEntity, TId>(
            this IServiceCollection services)
            where TId : struct, IEquatable<TId>
            where TEntity : class, IEntity<TId>, IAuditableEntity
            => services.AddScoped<IRepository<TEntity, TId>, Repository<TEntity, TId>>();
    }
}
