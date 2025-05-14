using CNAB.Domain.Entities;
using CNAB.Domain.Interfaces;
using Moq;

namespace CNAB.Domain.Test.Interfaces;

public class StoreRepositoryTest
{
    [Fact(DisplayName = "GetByNameAsync - Should Return Store when name exists")]
    public async Task Store_GetByNameAsync_ShouldReturnStoreWhenNameExists()
    {
        // Arrange
        var storeName = "Store A";
        var expectedStore = new Store(Guid.NewGuid(), storeName, "Owner of Store A");

        var mockStoreRepository = new Mock<IStoreRepository>();
        mockStoreRepository.Setup(repo => repo.GetByNameAsync(storeName))
            .ReturnsAsync(expectedStore);

        // Act
        var result = await mockStoreRepository.Object.GetByNameAsync(storeName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedStore.Id, result.Id);
        Assert.Equal(expectedStore.Name, result.Name);
        Assert.Equal(expectedStore.OwnerName, result.OwnerName);
    }

    [Fact(DisplayName = "GetByNameAsync - Should return Store when name does not exist")]
    public async Task Store_GetByNameAsync_ShouldReturnStoreWhenNameDoesNotExist()
    {
        // Arrange
        var nonExistingStoreName = "Nonexistent Store";

        var mockStoreRepository = new Mock<IStoreRepository>();
        mockStoreRepository.Setup(repo => repo.GetByNameAsync(nonExistingStoreName))
            .ReturnsAsync((Store)null);

        // Act
        var result = await mockStoreRepository.Object.GetByNameAsync(nonExistingStoreName);

        // Assert
        Assert.Null(result);
    }
}