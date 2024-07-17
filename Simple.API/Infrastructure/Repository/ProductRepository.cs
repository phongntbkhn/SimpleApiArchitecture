
using Simple.API.Infrastructure.Entities;
using Simple.API.Infrastructure.Repository.Base;
using Simple.API.Infrastructure.Repository.IRepository;

namespace Simple.API.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Production>, IProductRepository
    {
        public ProductRepository(SimpleApiContext dbContext) : base(dbContext)
        {
        }
    }
}
