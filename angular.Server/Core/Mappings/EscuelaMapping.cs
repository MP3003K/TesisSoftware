using AutoMapper;
using Domain.Entities;
using DTOs;


namespace Application.Mappings
{
    public class EscuelaMapping: Profile
    {
        public EscuelaMapping()
        {
            CreateMap<Escuela, EscuelaDto>()
                .ReverseMap();
        }
    }
}
