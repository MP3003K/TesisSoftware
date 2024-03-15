using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class DocenteMapping : Profile
    {
        public DocenteMapping()
        {
            CreateMap<Docente, DocenteDto>().ReverseMap();
        }
    }
}
