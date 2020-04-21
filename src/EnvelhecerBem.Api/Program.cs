using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EnvelhecerBem.Api
{
    public class Program
    {
        private static IConfiguration _configuration;

        public static void Main(string[] args)
        {
            _configuration = BuildConfiguration(args);
            CreateLogger(_configuration);
            CreateHostBuilder(args).Build().Run();
        }

        static IConfiguration BuildConfiguration(string[] args)
            => new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, false)
               .Build();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseConfiguration(_configuration)
                                                                  .UseSerilog()
                                                                  .UseStartup<Startup>());

        private static void CreateLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .CreateLogger();
        }
    }
}