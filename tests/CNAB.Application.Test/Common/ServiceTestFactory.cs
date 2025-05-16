using CNAB.Application.DTOs;
using CNAB.Domain.Entities;

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
}