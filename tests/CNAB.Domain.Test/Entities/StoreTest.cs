using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;
using FluentAssertions;

namespace CNAB.Domain.Test.Entities;

public class StoreTest
{
    [Fact(DisplayName = "Constructor - Should create Store when valid data")]
    public void Store_Constructor_ShouldCreateStoreWhenValidData()
    {
        // Arrange
        var name = "Store 01";
        var owner = "John Smith";

        // Act
        var store = new Store(name, owner);

        // Assert
        store.Name.Should().Be(name);
        store.OwnerName.Should().Be(owner);
        store.Id.Should().NotBe(Guid.Empty);
    }

    [Fact(DisplayName = "Constructor - Should throw exception when name is empty")]
    public void Store_Constructor_ShouldThrowExceptionWhenNameIsEmpty()
    {
        // Arrange
        var invalidName = "";
        var owner = "John Smith";

        // Act
        var act = () => new Store(invalidName, owner);

        // Assert
        act.Should()
            .Throw<Exception>()
            .WithMessage("*Invalid name*");
    }

    [Fact(DisplayName = "AddTransaction - Should add Transaction to list")]
    public void Store_AddTransaction_ShouldAddTransactionToList()
    {
        // Arrange
        var store = new Store("Store 02", "Mary");
        var transaction = CreateValidTransaction(store);

        // Act
        store.AddTransaction(transaction);

        // Assert
        store.Transactions.Should().Contain(transaction);
    }

    [Fact(DisplayName = "AddTransaction - Should not throw when Transaction is valid")]
    public void Store_AddTransaction_ShouldNotThrowWhenTransactionIsValid()
    {
        // Arrange
        var store = new Store("Test Store", "Test Owner");
        var transaction = CreateValidTransaction(store);

        // Act
        Action act = () => store.AddTransaction(transaction);

        // Assert
        act.Should().NotThrow();
    }

    [Fact(DisplayName = "GetBalance - Should return correct sum of signed amounts")]
    public void Store_GetBalance_ShouldReturnCorrectSumOfSignedAmounts()
    {
        // Arrange
        var store = new Store("Test Store", "Test Owner");

        var test1 = CreateTransactionWithAmount(store, 100m, TransactionType.Debit);
        var test2 = CreateTransactionWithAmount(store, 50m, TransactionType.Bill);

        store.AddTransaction(test1);
        store.AddTransaction(test2);

        // Act
        var balance = store.GetBalance();

        // Assert
        balance.Should().Be(50);
    }

    [Fact(DisplayName = "GetBalance - Should return zero when no Transactions")]
    public void Store_GetBalance_ShouldReturnZeroWhenNoTransactions()
    {
        // Arrange
        var store = new Store("Test Store", "Test Owner");

        // Act
        var balance = store.GetBalance();

        // Assert
        balance.Should().Be(0);
    }

    [Fact(DisplayName = "UpdateDetails - Should update Store name and owner")]
    public void Store_UpdateDetails_ShouldUpdateStoreNameAndOwner()
    {
        // Arrange
        var store = new Store("Old Store", "Old Owner");

        // Act
        store.UpdateDetails("New Store", "New Owner");

        // Assert
        store.Name.Should().Be("New Store");
        store.OwnerName.Should().Be("New Owner");
    }

    [Fact(DisplayName = "UpdateDetails - Should throw exception when name in valid")]
    public void Store_UpdateDetails_ShouldThrowExceptionWhenNameInvalid()
    {
        // Arrange
        var store = new Store("Initial Store", "Initial Owner");

        // Act
        var act = () => store.UpdateDetails("", "New Owner");

        // Assert
        act.Should()
            .Throw<Exception>()
            .WithMessage("*Invalid name*");
    }


    private Transaction CreateValidTransaction(Store store)
    {
        return new Transaction(
            TransactionType.Credit,
            DateTime.Now,
            100.00m,
            "12345678901",
            "1234****5678",
            new TimeSpan(14, 30, 0),
            store
        );
    }

    private Transaction CreateTransactionWithAmount(Store store, decimal amount, TransactionType type)
    {
        return new Transaction(
            type,
            DateTime.Now.Date,
            amount,
            "12345678901",
            "1234****5678",
            TimeSpan.FromHours(12),
            store);
    }
}