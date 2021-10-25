using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prometheus.DotNetRuntime;

namespace Ecommerce.Services.Orders.Infrastructure.Metrics.HostedServices
{
    internal sealed class PrometheusHostedService : IHostedService
    {
        private IDisposable _collector;
        private readonly ILogger<PrometheusHostedService> _logger;
        private readonly bool _enabled;

        public PrometheusHostedService(IOptions<PrometheusOptions> options,
			ILogger<PrometheusHostedService> logger)
        {
            _enabled = options.Value.Enabled;
            _logger = logger;
            _logger.LogInformation($"Prometheus integration is {(_enabled ? "enabled" : "disabled")}.");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_enabled)
            {
                _collector = DotNetRuntimeStatsBuilder
                    .Customize()
                    .WithContentionStats()
                    .WithJitStats()
                    .WithThreadPoolStats()
                    .WithThreadPoolStats()
                    .WithGcStats()
                    .WithExceptionStats()
                    .StartCollecting();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _collector?.Dispose();

            return Task.CompletedTask;
        }
    }
}
