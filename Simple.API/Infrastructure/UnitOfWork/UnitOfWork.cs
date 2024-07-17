using Microsoft.EntityFrameworkCore;
using Simple.API.Infrastructure.Repository;
using Simple.API.Infrastructure.Repository.IRepository;

namespace Simple.API.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleApiContext _context;

        public IProductRepository IProductRepository => new ProductRepository(_context);

        public UnitOfWork(SimpleApiContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public void Rollback()
        {
            _context.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
