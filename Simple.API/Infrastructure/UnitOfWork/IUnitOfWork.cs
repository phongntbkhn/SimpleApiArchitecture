
using Simple.API.Infrastructure.Repository.IRepository;

namespace Simple.API.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository IProductRepository { get; }
        void Save();
        Task SaveAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
