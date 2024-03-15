using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class GradoEvaPsicologicaMapping : Profile
    {
        public GradoEvaPsicologicaMapping()
        {
            CreateMap<GradoEvaPsicologica, GradoEvaPsicologicaDto>().ReverseMap();
        }
    }
}
