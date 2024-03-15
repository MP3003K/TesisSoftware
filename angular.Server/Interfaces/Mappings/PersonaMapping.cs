using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class PersonaMapping : Profile
    {
        public PersonaMapping()
        {
            CreateMap<Persona, PersonaDto>().ReverseMap();
        }
    }
}
