using System.Threading.Tasks;

namespace EnvelhecerBem.Core.Data
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}