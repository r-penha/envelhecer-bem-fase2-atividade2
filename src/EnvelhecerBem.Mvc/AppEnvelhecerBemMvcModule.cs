using System.Data;
using EnvelhecerBem.Core.Data;
using EnvelhecerBem.Core.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnvelhecerBem.Mvc
{
    public static class AppEnvelhecerBemMvcModule
    {
        public static IMvcBuilder AddEnvelhecerBemMvcModule(this IMvcBuilder builder, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            builder.AddApplicationPart(typeof(AppEnvelhecerBemMvcModule).Assembly);

            builder.Services
                   .AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString))
                   .AddScoped<IDbConnection>(c => new SqlConnection(connectionString))
                   .AddScoped<IUnitOfWork, EfCoreUnitOfWork>()
                   .AddScoped<IParceiroRepository, ParceiroRepository>()
                   .AddScoped<IClienteRepository, ClienteRepository>()
                   .AddScoped<ParceiroApplicationService>()
                   .AddScoped<ClienteApplicationService>();

            return builder;
        }
    }
}