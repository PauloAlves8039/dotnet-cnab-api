using CNAB.Domain.Entities;
using CNAB.Domain.Interfaces;
using CNAB.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CNAB.Infra.Data.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context, ILogger<Transaction> logger) : base(context, logger) { }

    public override async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        try
        {
            return await _context.Set<Transaction>()
                .Include(t => t.Store)
                .ToListAsync();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError($"Error when searching list of records: {exception.Message}");
            throw;
        }
    }

    public override async Task<Transaction> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Set<Transaction>()
                .Include(t => t.Store)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError($"Error when searching record by ID '{id}' {exception.Message}");
            throw;
        }
    }
}