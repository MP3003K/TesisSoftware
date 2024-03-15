using AutoMapper;
using Entities;
using DTOs;


namespace Interfaces.Mappings
{
    public class AulaMapping : Profile
    {
        public AulaMapping()
        {
            CreateMap<Aula, AulaDto>().ReverseMap();
        }
    }
}
