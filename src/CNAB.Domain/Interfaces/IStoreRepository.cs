using CNAB.Domain.Entities;

namespace CNAB.Domain.Interfaces;

public interface IStoreRepository : IRepository<Store> 
{
    Task<Store> GetByNameAsync(string storeName);
}