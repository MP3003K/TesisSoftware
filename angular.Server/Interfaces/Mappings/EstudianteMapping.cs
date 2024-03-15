using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class EstudianteMapping : Profile
    {
        public EstudianteMapping()
        {
            CreateMap<Estudiante, EstudianteDto>().ReverseMap();
        }
    }
}
