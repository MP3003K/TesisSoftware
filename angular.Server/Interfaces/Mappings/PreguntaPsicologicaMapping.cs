using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class PreguntaPsicologicaMapping : Profile
    {
        public PreguntaPsicologicaMapping()
        {
            CreateMap<PreguntaPsicologica, PreguntaPsicologicaDto>().ReverseMap();
        }
    }
}
