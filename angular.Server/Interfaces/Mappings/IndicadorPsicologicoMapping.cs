using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class IndicadorPsicologicoMapping : Profile
    {
        public IndicadorPsicologicoMapping()
        {
            CreateMap<IndicadorPsicologico, IndicadorPsicologicoDto>().ReverseMap();
        }
    }
}
