using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class GradoMapping : Profile
    {
        public GradoMapping()
        {
            CreateMap<Grado, GradoDto>().ReverseMap();
        }
    }
}
