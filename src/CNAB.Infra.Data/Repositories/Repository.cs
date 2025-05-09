using CNAB.Domain.Interfaces;
using CNAB.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CNAB.Infra.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly ILogger<TEntity> _logger;

    public Repository(ApplicationDbContext context, ILogger<TEntity> logger)
    {
        _context = context;
        _logger = logger;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _context.Set<TEntity>().ToListAsync();

        if (entities == null || !entities.Any())
        {
            _logger.LogWarning($"No records found for entity type: {typeof(TEntity).Name}");
            return Enumerable.Empty<TEntity>();
        }

        return entities;
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);

        if (entity == null)
        {
            _logger.LogWarning($"No record found for ID: {id}");
        }

        return entity;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException exception)
        {
            _logger.LogError($"Error when adding a new record: {exception.Message}");
            throw;
        }
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException exception)
        {
            _logger.LogError($"Error when updating the record: {exception.Message}");
            throw;
        }
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        catch (DbUpdateException exception)
        {
            _logger.LogError($"Error when deleting the record: {exception.Message}");
            throw;
        }
    }
}