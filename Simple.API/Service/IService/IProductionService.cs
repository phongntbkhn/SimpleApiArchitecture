using Simple.API.Infrastructure.Entities;

namespace Simple.API.Service.IService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Production>? GetIQueryable(string? name);
    }
}
