using CNAB.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CNAB.Infra.Data.Test.Common;

public class IntegrationTestBase : IDisposable
{
    protected readonly ApplicationDbContext DbContext;
    private readonly string _databaseName;

    public IntegrationTestBase()
    {
        _databaseName = Guid.NewGuid().ToString();
        
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: _databaseName)
            .Options;

        DbContext = new ApplicationDbContext(options);
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}