using System;
using EnvelhecerBem.Core.Domain;

namespace EnvelhecerBem.Core.Data
{
    public interface IClienteRepository : IRepository<Cliente, Guid>
    {
    }

    public class ClienteRepository :  Repository<Cliente, Guid>, IClienteRepository
    {
        public ClienteRepository(AppDbContext dbContext) : base(dbContext.Clientes)
        {
        }
    }
}