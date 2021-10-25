using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using MediatR;
using OpenTracing;
using OpenTracing.Util;
using OpenTracing.Contrib.NetCore.Configuration;
using Ecommerce.Services.Orders.Infrastructure.Tracing.Pipelines;
using ISender = Jaeger.Senders.ISender;

namespace Ecommerce.Services.Orders.Infrastructure.Tracing
{
	internal static class DependencyInjection
	{
		private const string SectionName = "jaeger";

		internal static IServiceCollection AddTracing(
			this IServiceCollection services)
		{
			var options = GetJaegerOptions(services);
			if (!options.Enabled)
			{
				var defaultTracer = DefaultTracer.Create();
				services.AddSingleton<ITracer>(defaultTracer);
				return services;
			}

			if (options.ExcludePaths is not null)
            {
                services.Configure<AspNetCoreDiagnosticOptions>(o =>
                {
                    foreach (var path in options.ExcludePaths)
                    {
                        o.Hosting
							.IgnorePatterns
							.Add(x => x.Request
									.Path == path);
                    }
                });
            }

			services.AddOpenTracing();
			services.AddSingleton<ITracer>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
				var maxPacketSize = options.MaxPacketSize > 0
					? options.MaxPacketSize
					: 64967;
                var senderType = string.IsNullOrWhiteSpace(options.Sender)
					? "udp"
					: options.Sender?.ToLowerInvariant();

				ISender sender = senderType switch
                {
                    "http" => BuildHttpSender(options.HttpSender),
                    "udp" => new UdpSender(options.UdpHost,
                                           options.UdpPort,
                                           maxPacketSize),
                    _ => throw new Exception($"Invalid Jaeger sender type: '{senderType}'.")
                };

                var reporter = new RemoteReporter.Builder()
                    .WithSender(sender)
                    .WithLoggerFactory(loggerFactory)
                    .Build();

                var sampler = GetSampler(options);

                var tracer = new Tracer.Builder(options.ServiceName)
					.WithLoggerFactory(loggerFactory)
                    .WithReporter(reporter)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });
            services.AddScoped(typeof(IPipelineBehavior<,>),
					typeof(JaegerPipeline<,>));

			return services;
		}

		private static JaegerOptions GetJaegerOptions(IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

			var section = configuration.GetSection(SectionName);
            services.Configure<JaegerOptions>(section);

			var options = new JaegerOptions();
			section.Bind(options);
			return options;
        }

		private static HttpSender BuildHttpSender(HttpSenderOptions options)
        {
            if (options is null)
            {
                throw new Exception("Missing Jaeger HTTP sender options.");
            }

            if (string.IsNullOrWhiteSpace(options.Endpoint))
            {
                throw new Exception("Missing Jaeger HTTP sender endpoint.");
            }

            var builder = new HttpSender.Builder(options.Endpoint);
            if (options.MaxPacketSize > 0)
            {
                builder = builder.WithMaxPacketSize(options.MaxPacketSize);
            }

            if (!string.IsNullOrWhiteSpace(options.AuthToken))
            {
                builder = builder.WithAuth(options.AuthToken);
            }

            if (!string.IsNullOrWhiteSpace(options.Username)
				&& !string.IsNullOrWhiteSpace(options.Password))
            {
                builder = builder.WithAuth(options.Username, options.Password);
            }

            if (!string.IsNullOrWhiteSpace(options.UserAgent))
            {
                builder = builder.WithUserAgent(options.Username);
            }

            return builder.Build();
        }

		private static ISampler GetSampler(JaegerOptions options)
        {
            switch (options.Sampler)
            {
                case "const":
					return new ConstSampler(true);
                case "rate":
					return new RateLimitingSampler(options.MaxTracesPerSecond);
                case "probabilistic":
					return new ProbabilisticSampler(options.SamplingRate);
                default: return new ConstSampler(true);
            }
        }
	}
}
