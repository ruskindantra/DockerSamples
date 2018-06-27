using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DockerConsole
{
    internal class App : IHostedService, IDisposable
    {
        private readonly ILogger<App> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IConfiguration _configuration;

        public App(ILogger<App> logger, IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _logger = logger;
            _appSettings = appSettings;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Variable SettingA has a value of <{_configuration["SettingA"]}>");

            _logger.LogInformation($"Starting with settings <{JsonConvert.SerializeObject(_appSettings)}>");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing.");
        }
    }
}