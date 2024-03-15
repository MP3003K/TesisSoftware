using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class EvaluacionPsicologicaEstudianteMapping : Profile
    {
        public EvaluacionPsicologicaEstudianteMapping()
        {
            CreateMap<EvaluacionPsicologicaEstudiante, EvaluacionPsicologicaEstudianteDto>().ReverseMap();
        }
    }
}
