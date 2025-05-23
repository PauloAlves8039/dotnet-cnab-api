using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;
using CNAB.Domain.Interfaces;
using CNAB.Domain.Test.Common;
using CNAB.Domain.Validations;
using Moq;

namespace CNAB.Domain.Test.Interfaces;

public class RepositoryTest
{

    [Fact(DisplayName = "GetAllAsync - Should return all transactions")]
    public async Task Repository_GetAllAsync_ShouldReturnAllTransactions()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var listTransactions = EntityTestFactory.GenerateMockTransactions();
        repositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(listTransactions);
        var _repository = repositoryMock.Object;

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(listTransactions.Count, result.Count());
        Assert.True(listTransactions.SequenceEqual(result));
    }

    [Fact(DisplayName = "GetAllAsync - Should return empty list when no Transactions")]
    public async Task Repository_GetAllAsync_ShouldReturnEmptyListWhenNoTransactions()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        repositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Transaction>());
        var _repository = repositoryMock.Object;

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact(DisplayName = "GetByIdAsync - Should return Transaction when found")]
    public async Task Repository_GetByIdAsync_ShouldReturnTransactionWhenFound()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var transaction = EntityTestFactory.CreateValidTransaction();
        repositoryMock.Setup(repo => repo.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);
        var _repository = repositoryMock.Object;

        // Act
        var result = await _repository.GetByIdAsync(transaction.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
    }

    [Fact(DisplayName = "GetByIdAsync - Should return Null when not found")]
    public async Task Repository_GetByIdAsync_ShouldReturnNullWhenNotFound()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var nonExistentId = Guid.NewGuid();
        repositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync((Transaction)null);
        var _repository = repositoryMock.Object;

        // Act
        var result = await _repository.GetByIdAsync(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "AddAsync - Should add Transaction successfully")]
    public async Task Repository_AddAsync_ShouldAddTransactionSuccessfully()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var transaction = EntityTestFactory.CreateValidTransaction();
        repositoryMock.Setup(repo => repo.AddAsync(transaction)).ReturnsAsync(transaction);
        var _repository = repositoryMock.Object;

        // Act
        var result = await _repository.AddAsync(transaction);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
    }

    [Fact(DisplayName = "AddAsync - Should not call repository when Transaction is invalid")]
    public async Task Repository_AddAsync_ShouldNotCallRepositoryWhenTransactionIsInvalid()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();

        Transaction invalidTransaction = null;

        // Act
        var exception = await Assert.ThrowsAsync<DomainExceptionValidation>(() =>
        {
            invalidTransaction = new Transaction(
                Guid.Empty,
                TransactionType.Credit,
                DateTime.Now,
                0m,
                "",
                "",
                TimeSpan.Zero,
                null
            );
            return repositoryMock.Object.AddAsync(invalidTransaction);
        });

        // Assert
        Assert.Equal("Invalid Id, Id cannot be empty", exception.Message);
        repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact(DisplayName = "UpdateAsync - Should update Transaction successfully")]
    public async Task Repository_UpdateAsync_ShouldUpdateTransactionSuccessfully()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var transaction = EntityTestFactory.CreateValidTransaction();
        repositoryMock.Setup(repo => repo.UpdateAsync(transaction)).ReturnsAsync(transaction);
        var _repository = repositoryMock.Object;

        // Act
        var result = await _repository.UpdateAsync(transaction);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
    }

    [Fact(DisplayName = "UpdateAsync - Should throw exception when update fails")]
    public async Task Repository_UpdateAsync_ShouldThrowExceptionWhenUpdateFails()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var transaction = EntityTestFactory.CreateValidTransaction();
        repositoryMock.Setup(repo => repo.UpdateAsync(transaction)).ThrowsAsync(new Exception("Failed to update"));
        var _repository = repositoryMock.Object;

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.UpdateAsync(transaction));
    }

    [Fact(DisplayName = "DeleteAsync - Should delete Transaction successfully")]
    public async Task Repository_DeleteAsync_ShouldDeleteTransactionSuccessfully()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var transaction = EntityTestFactory.CreateValidTransaction();
        repositoryMock.Setup(repo => repo.DeleteAsync(transaction.Id)).Returns(Task.CompletedTask);
        var _repository = repositoryMock.Object;

        // Act
        await _repository.DeleteAsync(transaction.Id);

        // Assert
        repositoryMock.Verify(repo => repo.DeleteAsync(transaction.Id), Times.Once);
    }

    [Fact(DisplayName = "DeleteAsync - Should throw exception when deletion fails")]
    public async Task Repository_DeleteAsync_ShouldThrowExceptionWhenDeletionFails()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<Transaction>>();
        var transactionId = Guid.NewGuid();
        repositoryMock.Setup(repo => repo.DeleteAsync(transactionId)).ThrowsAsync(new Exception("Failed to delete"));
        var _repository = repositoryMock.Object;

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _repository.DeleteAsync(transactionId));
    }
}