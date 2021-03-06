using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using static System.Environment;

namespace EnvelhecerBem.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = BuildConfiguration(args);

            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .CreateLogger();

            CreateWebHost(configuration).Build().Run();
        }

        static IConfiguration BuildConfiguration(string[] args)
            => new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, false)
               .Build();

        static IWebHostBuilder CreateWebHost(
            IConfiguration configuration)
            => new WebHostBuilder()
               .UseStartup<Startup>()
               .UseConfiguration(configuration)
               .UseContentRoot(CurrentDirectory)
               .UseSerilog()
               .UseKestrel();
    }
}
