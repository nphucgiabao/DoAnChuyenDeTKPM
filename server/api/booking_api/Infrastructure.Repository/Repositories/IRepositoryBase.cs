using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace booking_api.Infrastructure.Repository.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Get(Expression<Func<TEntity, bool>> filter = null,
            string[] includeProperties = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            string[] includeProperties = null,
            CancellationToken cancellationToken = default);

        List<TEntity> GetAll(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default,
            string[] includeProperties = null);

        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = default,
            string[] includeProperties = null,
            CancellationToken cancellationToken = default);

       
        void Insert(TEntity entity);
        void InsertMultiple(List<TEntity> entities);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
