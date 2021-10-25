using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Services.Orders.Core.Entities;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
	}
}
