using CNAB.Application.DTOs;
using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;

namespace CNAB.Application.Test.Common;

public static class ServiceTestFactory
{
    public static List<Store> GenerateMockStores()
    {
        return new List<Store>
        {
            new Store("Store A", "Owner A"),
            new Store("Store B", "Owner B"),
            new Store("Store C", "Owner C")
        };
    }

    public static StoreInputDto CreateStoreInputDto()
    {
        return new StoreInputDto
        {
            Name = "New Store",
            OwnerName = "New Owner"
        };
    }

    public static StoreInputDto UpdateStoreInputDto(Guid storeId)
    {
        return new StoreInputDto
        {
            Id = storeId,
            Name = "Updated Store",
            OwnerName = "Updated Owner"
        };
    }

    public static StoreDto CreateStoreDto(Guid storeId, string name, string ownerName)
    {
        return new StoreDto
        {
            Id = storeId,
            Name = name,
            OwnerName = ownerName,
            Balance = 0m
        };
    }

    public static Transaction CreateTransaction(Guid id)
    {
        var store = new Store("Test Store", "Test Owner");

        var transaction = new Transaction(
            TransactionType.Debit,
            DateTime.Now,
            100m,
            "12345678901",
            "1234567890123456",
            new TimeSpan(10, 30, 0),
            store);

        typeof(Entity).GetProperty("Id").SetValue(transaction, id);

        return transaction;
    }

    public static Transaction UpdateTransaction(Guid storeId, Store store)
    {
        var transaction = new Transaction(
            TransactionType.Debit,
            DateTime.Now.AddDays(-1),
            100m,
            "12345678901",
            "1234567890123456",
            new TimeSpan(10, 30, 0),
            store);

        return transaction;
    }

    public static TransactionDto CreateTransactionDto(Guid id)
    {
        return new TransactionDto
        {
            Id = id,
            Type = (int)TransactionType.Debit,
            OccurrenceDate = DateTime.Now,
            Amount = 100m,
            CPF = "12345678901",
            CardNumber = "1234567890123456",
            Time = new TimeSpan(10, 30, 0),
            StoreId = Guid.NewGuid(),
            StoreName = "Test Store",
            StoreOwnerName = "Test Owner"
        };
    }

    public static TransactionDto UpdateTransactionDto(Guid transactionId, Guid storeId)
    {
        var transactionDto = new TransactionDto
        {
            Id = transactionId,
            Type = (int)TransactionType.Credit,
            OccurrenceDate = DateTime.Now,
            Amount = 200m,
            CPF = "09876543210",
            CardNumber = "6543210987654321",
            Time = new TimeSpan(14, 45, 0),
            StoreId = storeId,
            StoreName = "Test Store",
            StoreOwnerName = "Test Owner"
        };

        return transactionDto;
    }
}