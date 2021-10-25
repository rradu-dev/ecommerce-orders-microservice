using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;
using Ecommerce.Services.Orders.Application.Pipelines;

namespace Ecommerce.Services.Orders.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
			this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMappings(assembly);
            services.AddValidations(assembly);
            services.AddMediatr(assembly);
            return services;
        }

        private static IServiceCollection AddMediatr(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.AddMediatR(assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>),
				typeof(LoggingPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>),
				typeof(UnhandledExceptionPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>),
				typeof(ValidationPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>),
				typeof(PerformancePipeline<,>));
            return services;
        }

        private static IServiceCollection AddValidations(
            this IServiceCollection services,
            Assembly assembly)
        {
			services.AddFluentValidation(options =>
			{
				options.AutomaticValidationEnabled = false;
				options.DisableDataAnnotationsValidation = true;
				options.ImplicitlyValidateChildProperties = true;
				options.ImplicitlyValidateRootCollectionElements = true;
				options.LocalizationEnabled = false;
				options.RegisterValidatorsFromAssembly(assembly);
			});
            return services;
        }

        private static IServiceCollection AddMappings(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
