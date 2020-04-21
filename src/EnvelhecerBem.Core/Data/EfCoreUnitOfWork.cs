using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EnvelhecerBem.Core.Data
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public EfCoreUnitOfWork(AppDbContext dbContext) => _dbContext = dbContext;

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}