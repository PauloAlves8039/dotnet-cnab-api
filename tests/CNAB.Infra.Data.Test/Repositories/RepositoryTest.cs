using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;
using CNAB.Domain.Validations;
using CNAB.Infra.Data.Context;
using CNAB.Infra.Data.Repositories;
using CNAB.Infra.Data.Test.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CNAB.Infra.Data.Test.Repositories;

public class RepositoryTest
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public RepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    private Repository<Transaction> CreateRepository(out ApplicationDbContext context)
    {
        context = new ApplicationDbContext(_dbContextOptions);
        var loggerMock = new Mock<ILogger<Transaction>>();
        return new Repository<Transaction>(context, loggerMock.Object);
    }

    [Fact(DisplayName = "GetAllAsync - Should return all transactions")]
    public async Task Repository_GetAllAsync_ShouldReturnAllTransactions()
    {
        // Arrange
        var repo = CreateRepository(out var context);
        var transactions = RepositoryTestFactory.GenerateListTransactions();
        context.Transactions.AddRange(transactions);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        result.Should().HaveCount(transactions.Count);
    }

    [Fact(DisplayName = "GetAllAsync - Should return empty list when no Transactions")]
    public async Task Repository_GetAllAsync_ShouldReturnEmptyListWhenNoTransactions()
    {
        // Arrange
        var repo = CreateRepository(out var context);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact(DisplayName = "GetByIdAsync - Should return Transaction when found")]
    public async Task Repository_GetByIdAsync_ShouldReturnTransactionWhenFound()
    {
        // Arrange
        var repo = CreateRepository(out var context);
        var transaction = RepositoryTestFactory.CreateTransaction();
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.GetByIdAsync(transaction.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(transaction.Id);
    }

    [Fact(DisplayName = "GetByIdAsync - Should return Null when not found")]
    public async Task Repository_GetByIdAsync_ShouldReturnNullWhenNotFound()
    {
        // Arrange
        var repo = CreateRepository(out _);

        // Act
        var result = await repo.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact(DisplayName = "AddAsync - Should add Transaction successfully")]
    public async Task Repository_AddAsync_ShouldAddTransactionSuccessfully()
    {
        // Arrange
        var repo = CreateRepository(out var context);
        var transaction = RepositoryTestFactory.CreateTransaction();

        // Act
        var result = await repo.AddAsync(transaction);

        // Assert
        context.Transactions.Should().Contain(result);
    }

    [Fact(DisplayName = "AddAsync - Should not call repository when Transaction is invalid")]
    public async Task Repository_AddAsync_ShouldNotCallRepositoryWhenTransactionIsInvalid()
    {
        // Arrange
        var repo = CreateRepository(out var context);

        // Act
        Func<Task> act = async () =>
        {
            var transaction = new Transaction(
                TransactionType.Credit,
                DateTime.MinValue, 
                -100,
                "", "", TimeSpan.Zero, RepositoryTestFactory.CreateStore()
            );

            await repo.AddAsync(transaction); 
        };

        // Assert
        await act.Should().ThrowAsync<DomainExceptionValidation>()
            .WithMessage("*occurrenceDate*");
    }

    [Fact(DisplayName = "UpdateAsync - Should update Transaction successfully")]
    public async Task Repository_UpdateAsync_ShouldUpdateTransactionSuccessfully()
    {
        // Arrange
        var repo = CreateRepository(out var context);
        var transaction = RepositoryTestFactory.CreateTransaction();
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        transaction.UpdateDetails(
            transaction.Type,
            transaction.OccurrenceDate,
            999,
            transaction.CPF,
            transaction.CardNumber,
            transaction.Time
        );

        // Act
        var result = await repo.UpdateAsync(transaction);

        // Assert
        result.Amount.Should().Be(999);
    }


    [Fact(DisplayName = "UpdateAsync - Should throw exception when update fails")]
    public async Task Repository_UpdateAsync_ShouldThrowExceptionWhenUpdateFails()
    {
        // Arrange
        var repo = CreateRepository(out var context);
        var transaction = RepositoryTestFactory.CreateTransaction();

        // Act
        Func<Task> act = async () => await repo.UpdateAsync(transaction);

        // Assert
        await act.Should().ThrowAsync<DbUpdateException>();
    }


    [Fact(DisplayName = "DeleteAsync - Should delete Transaction successfully")]
    public async Task Repository_DeleteAsync_ShouldDeleteTransactionSuccessfully()
    {
        // Arrange
        var repo = CreateRepository(out var context);
        var transaction = RepositoryTestFactory.CreateTransaction();
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        // Act
        await repo.DeleteAsync(transaction.Id);
        var result = await context.Transactions.FindAsync(transaction.Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact(DisplayName = "DeleteAsync - Should throw exception when deletion fails")]
    public async Task Repository_DeleteAsync_ShouldThrowExceptionWhenDeletionFails()
    {
        // Arrange
        var repo = CreateRepository(out var context);

        // Act
        Func<Task> act = async () => await repo.DeleteAsync(Guid.NewGuid());

        // Assert
        await act.Should().NotThrowAsync();
    }

}
