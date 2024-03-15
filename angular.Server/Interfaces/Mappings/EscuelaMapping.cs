using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class EscuelaMapping : Profile
    {
        public EscuelaMapping()
        {
            CreateMap<Escuela, EscuelaDto>().ReverseMap();
        }
    }
}
