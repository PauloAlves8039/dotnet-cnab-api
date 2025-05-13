using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;
using FluentAssertions;

namespace CNAB.Domain.Test.Entities;

public class TransactionTest
{
    [Fact(DisplayName = "Constructor - Should create Transaction when data is valid")]
    public void Transaction_Constructor_ShouldCreateTransactionWhenDataIsValid()
    {
        // Arrange
        var store = new Store("Test Store", "John Doe");

        // Act
        var transaction = new Transaction(
            TransactionType.Credit,
            new DateTime(2024, 5, 10),
            100.50m,
            "12345678901",
            "1234****5678",
            new TimeSpan(14, 30, 0),
            store
        );

        // Assert
        transaction.Should().NotBeNull();
        transaction.Amount.Should().Be(100.50m);
        transaction.Type.Should().Be(TransactionType.Credit);
        transaction.CPF.Should().Be("12345678901");
        transaction.CardNumber.Should().Be("1234****5678");
        transaction.Store.Should().Be(store);
    }

    [Fact(DisplayName = "Constructor - Should throw exception when CPF is invalid")]
    public void Transaction_Constructor_ShouldThrowExceptionWhenCpfIsInvalid()
    {
        // Arrange
        var store = new Store("Test Store", "John Doe");

        // Act
        var act = () => new Transaction(
            TransactionType.Credit,
            DateTime.Now,
            100.50m,
            "123",
            "1234****5678",
            TimeSpan.FromHours(14),
            store
        );

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage("*must be 11 characters*");
    }

    [Fact(DisplayName = "UpdateDetails - Should update Transaction when data is valid")]
    public void Transaction_UpdateDetails_ShouldUpdateTransactionWhenDataIsValid()
    {
        // Arrange
        var store = new Store("Test Store", "John Doe");
        var transaction = CreateValidTransaction(store);

        // Act
        transaction.UpdateDetails(
            TransactionType.Sales,
            new DateTime(2024, 4, 5),
            250.75m,
            "98765432100",
            "9876****4321",
            new TimeSpan(9, 45, 0)
        );

        // Assert
        transaction.Type.Should().Be(TransactionType.Sales);
        transaction.Amount.Should().Be(250.75m);
        transaction.CPF.Should().Be("98765432100");
        transaction.CardNumber.Should().Be("9876****4321");
    }

    [Fact(DisplayName = "UpdateDetails - Should throw exception when amount is zero")]
    public void Transaction_UpdateDetails_ShouldThrowExceptionWhenAmountIsZero()
    {
        // Arrange
        var store = new Store("Test Store", "John Doe");
        var transaction = CreateValidTransaction(store);

        // Act
        var act = () => transaction.UpdateDetails(
            TransactionType.Credit,
            DateTime.Now,
            0m,
            "12345678901",
            "1234****5678",
            TimeSpan.FromHours(10)
        );

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage("*must be greater than zero*");
    }

    [Fact(DisplayName = "SignedAmount - Should return positive value when income Transaction")]
    public void Transaction_SignedAmount_ShouldReturnPositiveValueWhenIncomeTransaction()
    {
        // Arrange
        var store = new Store("Store", "Owner");
        var transaction = new Transaction(
            TransactionType.Credit,
            DateTime.Now,
            200m,
            "12345678901",
            "1234****5678",
            TimeSpan.FromHours(12),
            store
        );

        // Act
        var signedAmount = transaction.SignedAmount;

        // Assert
        signedAmount.Should().Be(200m);
    }

    [Fact(DisplayName = "SignedAmount - Should return negative value when expense Transaction")]
    public void Transaction_SignedAmount_ShouldReturnNegativeValueWhenExpenseTransaction()
    {
        // Arrange
        var store = new Store("Store", "Owner");
        var transaction = new Transaction(
            TransactionType.Bill,
            DateTime.Now,
            120m,
            "12345678901",
            "1234****5678",
            TimeSpan.FromHours(12),
            store
        );

        // Act
        var signedAmount = transaction.SignedAmount;

        // Assert
        signedAmount.Should().Be(-120m);
    }

    [Fact(DisplayName = "IsIncome - Should return true for income Transaction type")]
    public void Transaction_IsIncome_ShouldReturnTrueForIncomeTransactionType()
    {
        // Arrange
        var store = new Store("Store", "Owner");
        var transaction = new Transaction(
            TransactionType.Debit,
            DateTime.Now,
            99.99m,
            "12345678901",
            "1234****5678",
            TimeSpan.FromHours(12),
            store
        );

        // Assert
        transaction.IsIncome.Should().BeTrue();
        transaction.IsExpense.Should().BeFalse();
    }

    [Fact(DisplayName = "IsIncome - Should return true for non income Transaction type")]
    public void Transaction_IsExpense_ShouldReturnTrueForNonIncomeTransactionType()
    {
        // Arrange
        var store = new Store("Store", "Owner");
        var transaction = new Transaction(
            TransactionType.Bill,
            DateTime.Now,
            60.00m,
            "12345678901",
            "1234****5678",
            TimeSpan.FromHours(15),
            store
        );

        // Assert
        transaction.IsExpense.Should().BeTrue();
        transaction.IsIncome.Should().BeFalse();
    }

    private Transaction CreateValidTransaction(Store store)
    {
        return new Transaction(
            TransactionType.Credit,
            new DateTime(2024, 1, 1),
            100.00m,
            "12345678901",
            "1234****5678",
            new TimeSpan(12, 0, 0),
            store
        );
    }
}