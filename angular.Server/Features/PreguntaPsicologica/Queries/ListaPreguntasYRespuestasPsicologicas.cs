using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using DTOs;
using Interfaces.Repositories;
using MediatR;

namespace Application.Features.PreguntaPsicologica.Queries
{
    public class ListaPreguntasPsicologicasQuery: IRequest<Response<IList<PreguntaPsicologicaDto>>>
    {
        public int EvaPsiEstId { get; set; }
        public int UnidadId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class ListaPreguntasYRespuestasPsicologicasHandler : IRequestHandler<ListaPreguntasPsicologicasQuery, Response<IList<PreguntaPsicologicaDto>>>
    {
        private readonly IPreguntaPsicologicaRepository _preguntaPsicologicaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
   
        private readonly IMapper _mapper;

        public ListaPreguntasYRespuestasPsicologicasHandler(
                       IPreguntaPsicologicaRepository preguntaPsicologicaRepository,
                       IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
                                  IMapper mapper)
        {
            _preguntaPsicologicaRepository = preguntaPsicologicaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<PreguntaPsicologicaDto>>> Handle(ListaPreguntasPsicologicasQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize <= 0)
                throw new AtributoNecesitaSerMayorACeroException(nameof(request.PageNumber));
            if (request.PageSize <= 0)
                throw new AtributoNecesitaSerMayorACeroException(nameof(request.PageSize));
            
            if(await _evaluacionPsicologicaEstudianteRepository.VerificarTestPsicologicoCompleto(request.EvaPsiEstId) is true) throw new EvalucionPsicologicaRealizadaException();
            
            var evaPsiAula = await _evaluacionPsicologicaEstudianteRepository.EvaPsiAulaPorEvaPsiEstudianteId(request.EvaPsiEstId) ?? throw new EntidadNoEncontradaException(nameof(Entities.EvaluacionPsicologicaAula));
            
            var preguntasPsicologicas = await _preguntaPsicologicaRepository.PreguntaPsicologicasPaginadas(evaPsiAula.EvaluacionPsicologicaId, request.EvaPsiEstId, request.PageSize, request.PageNumber) ?? throw new EntidadNoEncontradaException(nameof(Entities.PreguntaPsicologica));
      
            if(!preguntasPsicologicas.Any()) throw new EntidadNoEncontradaException(nameof(Entities.PreguntaPsicologica));

            var preguntasPsicologicasDto = _mapper.Map<IList<PreguntaPsicologicaDto>>(preguntasPsicologicas);         
           
            return new Response<IList<PreguntaPsicologicaDto>>(preguntasPsicologicasDto);
        }
    }
}
