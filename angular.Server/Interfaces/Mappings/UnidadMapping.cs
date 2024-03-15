using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class UnidadMapping : Profile
    {
        public UnidadMapping()
        {
            CreateMap<Unidad, UnidadDto>().ReverseMap();
        }
    }
}
