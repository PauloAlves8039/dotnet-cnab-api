using CNAB.Application.DTOs;
using CNAB.Domain.Entities;
using CNAB.Domain.Entities.enums;
using Mapster;

namespace CNAB.Application.Mappings;

public class TransactionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Transaction, TransactionDto>()
            .Map(dest => dest.Type, src => (int)src.Type)
            .Map(dest => dest.StoreId, src => src.Store.Id)
            .Map(dest => dest.StoreName, src => src.Store.Name);

        config.NewConfig<TransactionDto, Transaction>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Type, src => (TransactionType)src.Type)
            .Map(dest => dest.OccurrenceDate, src => src.OccurrenceDate)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.CPF, src => src.CPF)
            .Map(dest => dest.CardNumber, src => src.CardNumber)
            .Map(dest => dest.Time, src => src.Time)
            .Ignore(dest => dest.Store);
    }
}