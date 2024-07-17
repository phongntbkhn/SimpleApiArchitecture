using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simple.API.Infrastructure.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable
         where TEntity : class
    {
        protected readonly SimpleApiContext _dataContext;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(SimpleApiContext dataContext)
        {
            _dataContext = dataContext;
            dbSet = _dataContext.Set<TEntity>();
        }

        public bool Contains(Expression<Func<TEntity, bool>> precidate)
        {
            return _dataContext.Set<TEntity>().Count(precidate) > 0;
        }

        public virtual TEntity Delete(object id)
        {
            var entity = _dataContext.Set<TEntity>().Find(id);
            if (entity == null)
                return entity;
            _dataContext.Set<TEntity>().Remove(entity);
            _dataContext.SaveChanges();
            return entity;
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = _dataContext.Set<TEntity>();
            return query;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            TEntity thisEntity = _dataContext.Set<TEntity>().Add(entity).Entity;
            if (_dataContext.SaveChanges() > 0)
            {
                return thisEntity;
            }

            return null;
        }

        public virtual TEntity Update(TEntity entity)
        {
            var local = _dataContext.Set<TEntity>().Local.FirstOrDefault();
            if (local != null)
                _dataContext.Entry(local).State = EntityState.Detached;

            _dataContext.Entry(entity).State = EntityState.Modified;
            if (_dataContext.SaveChanges() > 0)
            {
                return entity;
            }
            return null;
        }

        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            _dataContext.SaveChanges();
        }

        public void Save()
        {
            try
            {
                _dataContext.SaveChanges();
            }
            catch (NpgsqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (!(ex.InnerException?.Message ?? ex.Message).Contains("Cannot insert duplicate key"))
                    throw;
            }
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            foreach (var entity in entities)
            {
                _dataContext.Entry(entity).State = EntityState.Modified;
            }
            _dataContext.SaveChanges();
        }

        public void DeleteByInt(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            _dataContext.SaveChanges();
        }

        public bool Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            if (_dataContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _disposed = false;

        ~BaseRepository() =>
            Dispose();

        public void Dispose()
        {
            if (!_disposed)
            {
                _dataContext.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
