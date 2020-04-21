using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EnvelhecerBem.Core.Data
{
    public abstract class Repository<T, TId> : IRepository<T, TId> 
        where T : class
    {
        public DbSet<T> Collection { get; }

        protected Repository(DbSet<T> dbSet)
        {
            Collection = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public async Task<T> Load(TId id)
        {
            return await Collection.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await Collection.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Task.FromResult(Collection.Update(entity));
        }

        public async Task Delete(TId entityId)
        {
            var entity = await Collection.FindAsync(entityId);
            if (entity == null) return;
            await Task.FromResult(Collection.Remove(entity));
        }

        public Task<IEnumerable<T>> ListAll()
        {
            return Task.FromResult(Collection.AsEnumerable());
        }

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(Collection.Where(expression).AsEnumerable());
        }
    }
}