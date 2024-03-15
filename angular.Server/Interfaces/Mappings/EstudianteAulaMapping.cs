using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class EstudianteAulaMapping : Profile
    {
        public EstudianteAulaMapping()
        {
            CreateMap<EstudianteAula, EstudianteAulaDto>().ReverseMap();
        }
    }
}
