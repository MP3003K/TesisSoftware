using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class EvaluacionPsicologicaAulaMapping : Profile
    {
        public EvaluacionPsicologicaAulaMapping()
        {
            CreateMap<EvaluacionPsicologicaAula, EvaluacionPsicologicaAulaDto>().ReverseMap();
        }

    }
}
