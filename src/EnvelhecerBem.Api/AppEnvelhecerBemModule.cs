using System.Data;
using EnvelhecerBem.Core.Data;
using EnvelhecerBem.Core.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnvelhecerBem.Api
{
    public static class AppEnvelhecerBemApiModule
    {
        public static IMvcBuilder AddEnvelhecerBemApiModule(this IMvcBuilder builder, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            builder.AddApplicationPart(typeof(AppEnvelhecerBemApiModule).Assembly);

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