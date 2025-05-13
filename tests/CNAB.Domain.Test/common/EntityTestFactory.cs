using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;

namespace CNAB.Domain.Test.common;

public static class EntityTestFactory
{
    public static Store CreateStore(string name = "Default Store", string owner = "Default Owner")
    {
        return new Store(name, owner);
    }

    public static Transaction CreateValidTransaction(Store store = null)
    {
        store ??= CreateStore();

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

    public static Transaction CreateTransactionWithAmount(Store store, decimal amount, TransactionType type)
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