using ECommerce.Core.Entities.Base;
using System.Linq.Expressions;

namespace ECommerce.Application.IGenericRepositories
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllEntities();
        Task<IEnumerable<TEntity>> FindEntities(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetEntityById(Guid id);

        Task<bool> AddEntity(TEntity entity);
        Task<bool> AddEntities(IEnumerable<TEntity> entities);

        Task<bool> UpdateEntity(TEntity entity);

        Task<bool> DeleteEntityById(Guid id);
        Task<bool> DeleteEntity(TEntity entity);
        Task<bool> DeleteEntities(IEnumerable<TEntity> entities);
        Task<bool> DeleteEntities(Expression<Func<TEntity, bool>> expression);
    }
}
