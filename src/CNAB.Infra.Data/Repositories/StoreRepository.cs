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
        var stores = await _context.Set<Store>()
            .Include(s => s.Transactions)
            .ToListAsync();

        if (stores == null || !stores.Any())
        {
            _logger.LogWarning("No stores found in the database.");
            return Enumerable.Empty<Store>();
        }

        return stores;
    }

    public override async Task<Store> GetByIdAsync(Guid id)
    {
        var store = await _context.Set<Store>()
            .Include(s => s.Transactions)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (store == null)
        {
            _logger.LogWarning($"No store found with ID: {id}");
        }

        return store;
    }

    public async Task<Store> GetByNameAsync(string storeName)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(s => s.Name == storeName);

        if (store == null)
        {
            _logger.LogWarning($"No store found with name: {storeName}");
        }

        return store;
    }
}