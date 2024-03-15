using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class EvaluacionPsicologicaMapping : Profile
    {
        public EvaluacionPsicologicaMapping()
        {
            CreateMap<EvaluacionPsicologica, EvaluacionPsicologicaDto>().ReverseMap();
        }
    }
}
