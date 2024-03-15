using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using DTOs;
using MediatR;
using webapi.Dao.Repositories;

namespace Application.Features.Aula.Queries
{
    public class ListaEstudiantesDeUnAulaQuery : IRequest<Response<IList<EstudianteDto>>>
    {
        public int AulaId { get; set; }
        public int UnidadId { get; set; }
        public int DimensionId { get; set; }
    }
    public class ListaEstudiantesDeUnAulaHandler : IRequestHandler<ListaEstudiantesDeUnAulaQuery, Response<IList<EstudianteDto>>>
    {
        private readonly IEvaluacionPsicologicaRepository _evaluacionPsicologicaRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IMapper _mapper;

        public ListaEstudiantesDeUnAulaHandler(
            IEvaluacionPsicologicaRepository evaluacionPsicologicaRepository,
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IEvaluacionPsicologicaEstudianteRepository estudianteRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaRepository = evaluacionPsicologicaRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _evaluacionPsicologicaEstudianteRepository = estudianteRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<EstudianteDto>>> Handle(ListaEstudiantesDeUnAulaQuery request, CancellationToken cancellationToken)
        {
            var evaluacionPsicologicaAula= await _evaluacionPsicologicaAulaRepository.EvaPsiAulaIncluyendoListaEstudiante(request.AulaId, request.UnidadId) ?? throw new EntidadNoEncontradaException(nameof(Domain.Entities.EvaluacionPsicologicaAula));

            if (evaluacionPsicologicaAula?.EvaluacionPsicologica?.Id == 2 && request.DimensionId == 1)
                request.DimensionId = 3;
            else if (evaluacionPsicologicaAula?.EvaluacionPsicologica?.Id == 2 && request.DimensionId == 2)
                request.DimensionId = 4;

            if (evaluacionPsicologicaAula?.EvaluacionesPsicologicasEstudiante == null || evaluacionPsicologicaAula.EvaluacionesPsicologicasEstudiante.Count <= 0)
                throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaEstudiante));

            var listaEstudiantesDto = new List<EstudianteDto>();

            foreach (var evaluacion in evaluacionPsicologicaAula.EvaluacionesPsicologicasEstudiante)
            {
                if(evaluacion.Estudiante != null && evaluacion.Estudiante.Persona != null)
                {
                    var estudianteDto = _mapper.Map<EstudianteDto>(evaluacion.Estudiante);
                    estudianteDto.PromedioPsicologicoEstudiante = await ResultadoEstudiante(estudianteDto.Id, request.UnidadId, request.AulaId, request.DimensionId);
                    listaEstudiantesDto.Add(estudianteDto);
                }
            }
            return new Response<IList<EstudianteDto>>(listaEstudiantesDto);
        }

        private async Task<double> ResultadoEstudiante(int estudianteId, int unidadId, int aulaId, int dimensionId)
        {
            var evaPsiEst = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudiante(estudianteId, unidadId, aulaId) ?? throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaEstudiante));

            var respuestasEscalasPsicologicas = await _evaluacionPsicologicaRepository.ResultadosPsicologicosEstudiante(evaPsiEst.Id, evaPsiEst!.EvaluacionAula!.EvaluacionPsicologica!.Id, dimensionId);

            if(respuestasEscalasPsicologicas is null || respuestasEscalasPsicologicas.Any() is false)
                return 0.0;

            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(respuestasEscalasPsicologicas);
            var respuestasEstudianteDto = new RespuestasPsicologicasEstudianteDto { EscalasPsicologicas = escalasPsicologicasDto };

            CalcularResultadosEvaluacionPsicologica(respuestasEstudianteDto);
            return respuestasEstudianteDto.PromedioEvaluacionPsicologica;
        }

        private static void CalcularResultadosEvaluacionPsicologica(RespuestasPsicologicasEstudianteDto respuestasEstudianteDto)
        {
            foreach (var escalaDto in respuestasEstudianteDto.EscalasPsicologicas)
            {
                double? promedioEscala = null;

                if (escalaDto.IndicadoresPsicologicos != null && escalaDto.IndicadoresPsicologicos.Any())
                {
                    foreach (var indicador in escalaDto.IndicadoresPsicologicos)
                    {
                        if (indicador != null)
                        {
                            indicador.PromedioIndicador = indicador.PreguntasPsicologicas?
                                .Where(i => i.RespuestasPsicologicas != null && i.RespuestasPsicologicas.Any())
                                .Sum(i =>
                                {
                                    var primeraRespuesta = i?.RespuestasPsicologicas?.FirstOrDefault();
                                    if (primeraRespuesta != null && double.TryParse(primeraRespuesta.Respuesta, out double respuestaNumero))
                                        return respuestaNumero;
                                    return 0.0;
                                });
                            indicador.PreguntasPsicologicas = null;
                        }
                    }

                    var totalPromedioIndicadores = escalaDto.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioEscala = Math.Round(totalPromedioIndicadores / escalaDto.IndicadoresPsicologicos.Count, 4);

                }
                escalaDto.PromedioEscala = promedioEscala;
            }
            var totalPromedioEscalas = respuestasEstudianteDto.EscalasPsicologicas.Sum(i => i.PromedioEscala ?? 0.0);
            respuestasEstudianteDto.PromedioEvaluacionPsicologica = Math.Round(totalPromedioEscalas / respuestasEstudianteDto.EscalasPsicologicas.Count, 4);
        }

    }
}