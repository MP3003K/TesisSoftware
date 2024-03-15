using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class RespuestaPsicologicaMapping : Profile
    {
        public RespuestaPsicologicaMapping()
        {
            CreateMap<RespuestaPsicologica, RespuestaPsicologicaDto>().ReverseMap();
        }
    }
}
