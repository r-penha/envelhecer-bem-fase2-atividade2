using EnvelhecerBem.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnvelhecerBem.Core.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public AppDbContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<Parceiro> Parceiros { get; private set; }
        public DbSet<Cliente> Clientes { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parceiro>()
                        .OwnsOne(p => p.Endereco,
                                 a => a.ToTable("EnderecosParceiros"));

            modelBuilder.Entity<Cliente>()
                        .OwnsOne(p => p.Endereco,
                                 a => a.ToTable("EnderecosClientes"));
        }
    }
}