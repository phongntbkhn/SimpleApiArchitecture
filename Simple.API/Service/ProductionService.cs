using Simple.API.Infrastructure.Entities;
using Simple.API.Infrastructure.UnitOfWork;
using Simple.API.Service.IService;
using Simple.Common.Helper;

namespace Simple.API.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductionService : IProductionService
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProductionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Production>? GetIQueryable(string? name)
        {
            var result = _unitOfWork.IProductRepository.GetAllQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return result;
        }
    }
}
