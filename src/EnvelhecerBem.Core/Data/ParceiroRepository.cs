using System;
using EnvelhecerBem.Core.Domain;

namespace EnvelhecerBem.Core.Data
{
    public interface IParceiroRepository : IRepository<Parceiro, Guid>
    {
    }

    public class ParceiroRepository : Repository<Parceiro, Guid>, IParceiroRepository
    {
        public ParceiroRepository(AppDbContext dbContext) : base(dbContext.Parceiros)
        {
        }
    }
}