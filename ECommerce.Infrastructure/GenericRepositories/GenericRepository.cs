using ECommerce.Application.IGenericRepositories;
using ECommerce.Core.Entities.Base;
using ECommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.GenericRepositories;

public abstract class GenericRepository<TEntity, TKey>
    : IGenericRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    protected AppDbContext _context;
    protected DbSet<TEntity> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllEntities()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    public virtual async Task<IEnumerable<TEntity>> FindEntities(Expression<Func<TEntity, bool>> expression)
    {
        var entities = await _dbSet.Where(expression).ToListAsync();
        return entities;
    }

    public virtual async Task<TEntity> GetEntityById(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public virtual async Task<bool> AddEntity(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<bool> AddEntities(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return true;
    }

    public virtual async Task<bool> UpdateEntity(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public virtual async Task<bool> DeleteEntityById(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null) 
            return false;

        _dbSet.Remove(entity);
        return true;
    }

    public virtual async Task<bool> DeleteEntity(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        try
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public virtual async Task<bool> DeleteEntities(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return true;
    }

    public virtual async Task<bool> DeleteEntities(Expression<Func<TEntity, bool>> expression)
    {
        var entities = await _dbSet.Where(expression).ToListAsync();
        _dbSet.RemoveRange(entities);
        return true;
    }
}
