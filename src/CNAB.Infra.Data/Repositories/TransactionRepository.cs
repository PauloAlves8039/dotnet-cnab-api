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
        var transactions = await _context.Set<Transaction>()
            .Include(s => s.Store)
            .ToListAsync();

        if (transactions == null || !transactions.Any())
        {
            _logger.LogWarning("No transactions found in the database.");
            return Enumerable.Empty<Transaction>();
        }

        return transactions;
    }
    
    public override async Task<Transaction> GetByIdAsync(Guid id)
    {
        var transaction = await _context.Set<Transaction>()
            .Include(s => s.Store)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (transaction == null)
        {
            _logger.LogWarning($"No transaction found with ID: {id}");
        }

        return transaction;
    }
}