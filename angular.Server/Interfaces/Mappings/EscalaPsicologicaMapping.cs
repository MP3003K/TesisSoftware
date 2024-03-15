using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class EscalaPsicologicaMapping : Profile
    {
        public EscalaPsicologicaMapping()
        {
            CreateMap<EscalaPsicologica, EscalaPsicologicaDto>().ReverseMap();
        }
    }
}
