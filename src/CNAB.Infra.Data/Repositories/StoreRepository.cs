using CNAB.Domain.Entities;
using CNAB.Domain.Interfaces;
using CNAB.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CNAB.Infra.Data.Repositories;

public class StoreRepository : Repository<Store>, IStoreRepository
{
    public StoreRepository(ApplicationDbContext context, ILogger<Store> logger) : base(context, logger) { }

    public override async Task<IEnumerable<Store>> GetAllAsync()
    {
        try
        {
            return await _context.Set<Store>()
                .Include(s => s.Transactions)
                .ToListAsync();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError($"Error when searching list of records: {exception.Message}");
            throw;
        }
    }

    public override async Task<Store> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Set<Store>()
                .Include(s => s.Transactions)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError($"Error when searching record by ID '{id}': {exception.Message}");
            throw;
        }
    }

    public async Task<Store> GetByNameAsync(string storeName)
    {
        try
        {
            return await _context.Stores
                .FirstOrDefaultAsync(s => s.Name == storeName);
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError($"Error when searching Store by Name: '{storeName}': {exception.Message}");
            throw;
        }
    }
}