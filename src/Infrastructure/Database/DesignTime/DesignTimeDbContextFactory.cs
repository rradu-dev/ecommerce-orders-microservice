using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ecommerce.Services.Orders.Infrastructure.Database.Context;
using Ecommerce.Services.Orders.Infrastructure.Database.Extensions;

namespace Ecommerce.Services.Orders.Infrastructure.Database.DesignTime
{
    public class DesignTimeDbContextFactory
		: IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment
				.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path
					.Combine(Directory
						.GetParent(Directory.GetCurrentDirectory())
						.FullName, "Api"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
                .Build();

            string connectionString =
				configuration.GetConnectionString("orders");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.ConfigureDbContextOptions(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
