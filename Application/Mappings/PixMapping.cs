using AutoMapper;
using Domain.Entities;
using DTOs;

namespace Application.Mappings;

public class PixMapping : Profile
{
    public PixMapping()
    {
        CreateMap<Pix, PixDto>()
            .ReverseMap();
    }
}