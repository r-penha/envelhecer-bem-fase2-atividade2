using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnvelhecerBem.Core.Data
{
    public interface IRepository<T, in TId> 
        where T : class
    {
        Task<T> Load(TId id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(TId entityId);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> ListAll();
    }
}