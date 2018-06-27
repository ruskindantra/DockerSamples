using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DockerConsole
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Stage1();
            await Stage2(args);
        }

        private static async Task Stage2(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                    Log.Logger = new LoggerConfiguration()
                        //.Enrich.FromLogContext()
                        //.WriteTo.ColoredConsole()
                        .ReadFrom.Configuration(config.Build())
                        .CreateLogger();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));

                    services.AddSingleton<IHostedService, App>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    // serilog logging
                    logging.AddSerilog(dispose:true);

                    // traditional logging
                    //logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    //logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }

        private static void Stage1()
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            log.Information("Hello, Serilog!");

            Log.Logger = log;
            Log.Information("The global logger has been configured");
        }
    }
}
