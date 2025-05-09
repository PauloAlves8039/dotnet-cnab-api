using CNAB.Application.DTOs;
using CNAB.Domain.Entities;
using Mapster;

namespace CNAB.Application.Mappings;

public class StoreMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Store, StoreDto>()
            .Map(dest => dest.Balance, src => src.GetBalance());

        config.NewConfig<StoreDto, Store>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.OwnerName, src => src.OwnerName)
            .Ignore(dest => dest.Transactions);

        config.NewConfig<StoreInputDto, Store>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.OwnerName, src => src.OwnerName)
            .Ignore(dest => dest.Transactions);
    }
}