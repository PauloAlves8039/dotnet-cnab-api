using CNAB.Application.DTOs;
using CNAB.Application.Interfaces;
using CNAB.Application.Services;
using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;
using CNAB.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CNAB.Application.Test.Interfaces
{
    public class StoreServiceTest
    {
        private readonly Mock<IStoreRepository> _mockStoreRepository;
        private readonly Mock<ILogger<StoreService>> _mockLogger;
        private readonly IStoreService _storeService;

        public StoreServiceTest()
        {
            _mockStoreRepository = new Mock<IStoreRepository>();
            _mockLogger = new Mock<ILogger<StoreService>>();
            _storeService = new StoreService(_mockStoreRepository.Object, null, _mockLogger.Object);
        }

        List<StoreDto> listStores = new List<StoreDto>
        {
            new StoreDto
            {
                Id = Guid.Parse("b3cfa2d3-1d4e-4c5b-9b87-89e2c8691c0a"),
                Name = "Store A",
                OwnerName = "Owner A",
                Balance = 1500.75m
            },
            new StoreDto
            {
                Id = Guid.Parse("f4d8b6be-240d-4a5e-9a2f-87b45d2c8d27"),
                Name = "Store B",
                OwnerName = "Owner B",
                Balance = 2450.50m
            },
            new StoreDto
            {
                Id = Guid.Parse("a6b3c9f3-7d8f-4c2e-9e3a-8b5d6c7e2f8b"),
                Name = "Store C",
                OwnerName = "Owner C",
                Balance = 3200.00m
            }
        };

        [Fact(DisplayName = "GetAllStoreAsync - Should return all Stores")]
        public async Task StoreService_GetAllStoreAsync_ShouldReturnAllStores()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            mockStoreService.Setup(service => service.GetAllStoreAsync()).ReturnsAsync(listStores);
            var storeService = mockStoreService.Object;

            // Act
            var result = await storeService.GetAllStoreAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(listStores.Count, result.Count());
        }

        [Fact(DisplayName = "GetAllStoreAsync - Should return empty list when no Stores exist")]
        public async Task StoreService_GetAllStoreAsync_ShouldReturnEmptyListWhenNoStoresExist()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var emptyStores = new List<StoreDto>();
            mockStoreService.Setup(service => service.GetAllStoreAsync()).ReturnsAsync(emptyStores);
            var categoryService = mockStoreService.Object;

            // Act
            var result = await categoryService.GetAllStoreAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "GetByIdStoreAsync - Should return StoreDto when id exists")]
        public async Task StoreService_GetByIdStoreAsync_ShouldReturnStoreDtoWhenIdExists()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var storeId = Guid.Parse("60935a54-a295-40b8-a2c3-b6a32a466174");
            var storeDto = new StoreDto { Id = storeId, Name = "Store D", OwnerName = "Owner D", Balance = 4600.00m };
            mockStoreService.Setup(service => service.GetByIdStoreAsync(storeId)).ReturnsAsync(storeDto);
            var storeService = mockStoreService.Object;

            // Act
            var result = await storeService.GetByIdStoreAsync(storeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(storeId, result.Id);
        }

        [Fact(DisplayName = "GetByIdStoreAsync - Should return null when StoreDto Id does not exist")]
        public async Task StoreService_GetByIdStoreAsync_ShouldReturnNullWhenStoreDtoIdDoesNotExist()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var invalidStoreId = Guid.Parse("0a0a0a0a-0a0a-0a0a-0a0a-0a0a0a0a0a0a"); ;
            mockStoreService.Setup(service => service.GetByIdStoreAsync(invalidStoreId)).ReturnsAsync((StoreDto)null);
            var storeService = mockStoreService.Object;

            // Act
            var result = await storeService.GetByIdStoreAsync(invalidStoreId);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "AddStoreAsync - Should insert StoreInputDto and return added StoreDto")]
        public async Task StoreService_AddStoreAsync_ShouldInsertStoreInputDtoAndReturnAddedStoreDto()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var storeId = Guid.Parse("60935a54-a295-40b8-a2c3-b6a32a466174");
            var storeInputDto = new StoreInputDto { Id = storeId, Name = "Store D", OwnerName = "Owner D" };
            mockStoreService.Setup(service => service.AddStoreAsync(storeInputDto)).Verifiable();
            var storeService = mockStoreService.Object;

            // Act
            await storeService.AddStoreAsync(storeInputDto);

            // Assert
            mockStoreService.Verify();
        }

        [Fact(DisplayName = "AddStoreAsync - Should return null when added StoreInputDto fails")]
        public async Task StoreService_AddStoreAsync_ShouldReturnNullWhenAddedStoreInputDtoFails()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var storeId = Guid.Parse("0a0a0a0a-0a0a-0a0a-0a0a-0a0a0a0a0a0a");
            var storeInputDto = new StoreInputDto { Id = storeId, Name = null, OwnerName = null };
            mockStoreService.Setup(service => service.AddStoreAsync(storeInputDto))
                .ThrowsAsync(new InvalidOperationException("Livros"));
            var storeService = mockStoreService.Object;

            // Act
            Func<Task> act = async () => await storeService.AddStoreAsync(storeInputDto);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);
            mockStoreService.Verify(service => service.AddStoreAsync(storeInputDto), Times.Once);
        }

        [Fact(DisplayName = "UpdateStoreAsync - Should update return updated StoreInputDto")]
        public async Task StoreService_UpdateStoreAsync_ShouldUpdateReturnUpdatedStoreInputDto()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var storeId = Guid.Parse("a6b3c9f3-7d8f-4c2e-9e3a-8b5d6c7e2f8b");
            var updatedStoreInputDto = new StoreInputDto { Id = storeId, Name = "Store 3", OwnerName = "Owner 3" };
            mockStoreService.Setup(service => service.UpdateStoreAsync(updatedStoreInputDto)).Verifiable();
            var storeService = mockStoreService.Object;

            // Act
            await storeService.UpdateStoreAsync(updatedStoreInputDto);

            // Assert
            mockStoreService.Verify();
        }

        [Fact(DisplayName = "UpdateStoreAsync - Should return null when StoreInputDto not found")]
        public async Task StoreService_UpdateStoreAsync_ShouldReturnNullWhenStoreInputDtoNotFound()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var storeId = Guid.Parse("0a0a0a0a-0a0a-0a0a-0a0a-0a0a0a0a0a0a");
            var invalidStoreInputDto = new StoreInputDto { Id = storeId, Name = null, OwnerName = null };
            mockStoreService.Setup(service => service.UpdateStoreAsync(invalidStoreInputDto))
                .ThrowsAsync(new InvalidOperationException("Invalid Store update"));
            var storeService = mockStoreService.Object;

            // Act
            Func<Task> act = async () => await storeService.UpdateStoreAsync(invalidStoreInputDto);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact(DisplayName = "DeleteStoreAsync - Should remove Store and return removed Store")]
        public async Task CategoryService_DeleteStoreAsync_ShouldRemoveStoreAndReturnRemovedStore()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var storeId = Guid.Parse("60935a54-a295-40b8-a2c3-b6a32a466174");
            mockStoreService.Setup(service => service.DeleteStoreAsync(storeId)).Verifiable();
            var storeService = mockStoreService.Object;

            // Act
            await storeService.DeleteStoreAsync(storeId);

            // Assert
            mockStoreService.Verify();
        }

        [Fact(DisplayName = "DeleteStoreAsync - Should return null when Store not found")]
        public async Task CategoryService_DeleteStoreAsync_ShouldReturnNullWhenStoreNotFound()
        {
            // Arrange
            var mockStoreService = new Mock<IStoreService>();
            var invalidStoreId = Guid.Parse("0a0a0a0a-0a0a-0a0a-0a0a-0a0a0a0a0a0a");
            mockStoreService.Setup(service => service.DeleteStoreAsync(invalidStoreId))
                .ThrowsAsync(new InvalidOperationException("Invalid Store removal"));
            var storeService = mockStoreService.Object;

            // Act
            Func<Task> act = async () => await storeService.DeleteStoreAsync(invalidStoreId);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact(DisplayName = "GetStoreBalanceAsync - Should return correct balance when Store exists")]
        public async Task CategoryService_GetStoreBalanceAsync_ShouldReturnCorrectBalanceWhenStoreExists()
        {
            // Arrange
            var storeId = Guid.NewGuid();
            var store = new Store("StoreName", "OwnerName");
            store.AddTransaction(new Transaction(TransactionType.Debit, DateTime.Now, 100m, "12345678901", "1234", TimeSpan.Zero, store));
            store.AddTransaction(new Transaction(TransactionType.Credit, DateTime.Now, 50m, "12345678901", "1234", TimeSpan.Zero, store));
            _mockStoreRepository.Setup(repo => repo.GetByIdAsync(storeId)).ReturnsAsync(store);

            // Act
            var balance = await _storeService.GetStoreBalanceAsync(storeId);

            // Assert
            Assert.Equal(150m, balance);
        }

        [Fact(DisplayName = "GetStoreBalanceAsync - Should return zero when Store does not exist")]
        public async Task GetStoreBalanceAsync_ShouldReturnZeroWhenStoreDoesNotExist()
        {
            // Arrange
            var storeId = Guid.NewGuid();
            _mockStoreRepository.Setup(repo => repo.GetByIdAsync(storeId)).ReturnsAsync((Store)null);

            // Act
            var balance = await _storeService.GetStoreBalanceAsync(storeId);

            // Assert
            Assert.Equal(0m, balance);
        }
    }
}