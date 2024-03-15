using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class DimensionPsicologicaMapping : Profile
    {
        public DimensionPsicologicaMapping()
        {
            CreateMap<DimensionPsicologica, DimensionPsicologicaDto>().ReverseMap();
        }
    }
}
