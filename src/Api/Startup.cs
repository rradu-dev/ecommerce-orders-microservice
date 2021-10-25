using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Ecommerce.Services.Orders.Application;
using Ecommerce.Services.Orders.Infrastructure;
using Ecommerce.Services.Orders.Api.Middlewares;

namespace Ecommerce.Services.Orders.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
			Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
			services.AddOptions();
			services.ConfigureConfigServerClientOptions(Configuration);

            services.AddApplication();
            services.AddInfrastructure();
            services.AddHttpContextAccessor();

			services.AddTransient<UnhandledExceptionHandlerMiddleware>();

            services.AddCors(co =>
            {
                var corsPolicies = Configuration
                    .GetSection("cors")
                    ?.Get<Dictionary<string, CorsPolicy>>();

                var containsDefaultPolicy = (corsPolicies
					?.ContainsKey("Default"))
					.GetValueOrDefault();

                if (containsDefaultPolicy)
                {
                    co.DefaultPolicyName = "Default";
                }

                if (corsPolicies != null)
                {
                    foreach (var kv in corsPolicies)
                    {
                        co.AddPolicy(kv.Key, kv.Value);
                    }
                }
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddControllers(options => {
					options.SuppressAsyncSuffixInActionNames = false;
				})
				.AddNewtonsoftJson(options => {
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
					options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
					options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					options.SerializerSettings.Formatting = Formatting.Indented;
					options.SerializerSettings.Converters.Add(new StringEnumConverter());
			});
			services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "E-Commerce Wishlists",
                    Description = "Microservice for managing wishlists",
                    Contact = new OpenApiContact
                    {
                        Name = "Razvan Radu",
                        Email = "rradu08@outlook.com",
                        Url = new Uri("https://github.com/rradu-dev")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseSerilogRequestLogging();
            app.UseInfrastructure();
            app.UseRouting();
            app.UseCors();

			app.UseMiddleware<UnhandledExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
				endpoints.MapHealthChecks("/health");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wishlists API"));
        }
    }
}
