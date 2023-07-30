using AutoMapper;
using Domain.Entities;
using DTOs;

namespace Application.Mappings;

public class BankMapping : Profile
{
    public BankMapping()
    {
        CreateMap<Bank, BankDto>()
            .ReverseMap();
    }
}