using CNAB.Domain.Entities;
using CNAB.Domain.Interfaces;
using Moq;

namespace CNAB.Domain.Test.Interfaces;

public class StoreRepositoryTest
{
    [Fact(DisplayName = "GetStoreByName - Should Return Store when name exists")]
    public async Task Store_GetStoreByName_ShouldReturnStoreWhenNameExists()
    {
        // Arrange
        var storeName = "Store A";
        var expectedStore = new Store(Guid.NewGuid(), storeName, "Owner of Store A");

        var mockStoreRepository = new Mock<IStoreRepository>();
        mockStoreRepository.Setup(repo => repo.GetStoreByName(storeName))
            .ReturnsAsync(expectedStore);

        // Act
        var result = await mockStoreRepository.Object.GetStoreByName(storeName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedStore.Id, result.Id);
        Assert.Equal(expectedStore.Name, result.Name);
        Assert.Equal(expectedStore.OwnerName, result.OwnerName);
    }

    [Fact(DisplayName = "GetStoreByName - Should return Store when name does not exist")]
    public async Task Store_GetStoreByName_ShouldReturnStoreWhenNameDoesNotExist()
    {
        // Arrange
        var nonExistingStoreName = "Nonexistent Store";

        var mockStoreRepository = new Mock<IStoreRepository>();
        mockStoreRepository.Setup(repo => repo.GetStoreByName(nonExistingStoreName))
            .ReturnsAsync((Store)null);

        // Act
        var result = await mockStoreRepository.Object.GetStoreByName(nonExistingStoreName);

        // Assert
        Assert.Null(result);
    }
}