using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RespuestaPsicologica.Queries
{
    public class SumResPsiEstudianteQuery : IRequest<Response<IList<EscalaPsicologicaDto>>>
    {

        public int UnidadId { get; set; }
        public int AulaId { get; set; }
        public int DimensionId { get; set; }
    }
    public class SumResPsiEstudianteHandler : IRequestHandler<SumResPsiEstudianteQuery, Response<IList<EscalaPsicologicaDto>>>
    {
        private IEvaluacionPsicologicaRepository _evaluacionPsicologicaRepository;
        private IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IMapper _mapper;

        public SumResPsiEstudianteHandler(IEvaluacionPsicologicaRepository evaluacionPsicologicaRepository, IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository, IMapper mapper)
        {
            _evaluacionPsicologicaRepository = evaluacionPsicologicaRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _mapper = mapper;
        }

        Task<Response<IList<EscalaPsicologicaDto>>> IRequestHandler<SumResPsiEstudianteQuery, Response<IList<EscalaPsicologicaDto>>>.Handle(SumResPsiEstudianteQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
