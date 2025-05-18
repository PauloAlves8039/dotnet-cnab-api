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

    public static Store CreateStore(string name, string ownerName)
    {
        return new Store(name, ownerName);
    }

    public static Store CreateStore()
    {
        return new Store("JOHN'S Bar", "JOHN DOE");
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

    public static Transaction CreateTransaction()
    {
        var store = CreateStore();

        return new Transaction(
            type: TransactionType.Debit,
            occurrenceDate: new DateTime(2019, 3, 1, 15, 34, 53),
            amount: 142.00m,
            cpf: "00962067601",
            cardNumber: "74753****3153",
            time: new TimeSpan(15, 34, 53),
            store: store
        );
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

    public static string GetValidCnabLinePadded()
    {
        return GetValidCnabLine().PadRight(81, ' ');
    }

    public static string GetValidCnabLine()
    {
        return "1" +
               "20190301" +
               "0000014200" +
               "00962067601" +
               "74753****315" +
               "153453" +
               "JOHN DOE      " +      
               "JOHN'S Bar         "; 
    }
}