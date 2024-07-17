using System.Linq.Expressions;

namespace Simple.API.Infrastructure.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        T FirstOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        T Insert(T entity);
        void InsertRange(IEnumerable<T> entities);

        T Update(T entity);
        public void UpdateRange(IEnumerable<T> entities);

        T Delete(object id);
        bool Delete(T entity);
        public void DeleteRange(IEnumerable<T> entities);

        bool Contains(Expression<Func<T, bool>> precidate);
        void Save();
    }
}
