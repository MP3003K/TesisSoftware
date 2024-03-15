using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using DTOs;
using MediatR;
using webapi.Dao.Repositories;

namespace Application.Features.RespuestaPsicologica.Queries
{
    public class ResultadosPsicologicosAulaQuery : IRequest<Response<RespuestasPsicologicasAulaDto>>
    {
        public int AulaId { get; set; }
        public int DimensionId { get; set; }
        public int UnidadId { get; set; }
    }
    public class ResultadosPsicologicosAulaHandler : IRequestHandler<ResultadosPsicologicosAulaQuery, Response<RespuestasPsicologicasAulaDto>>
    {
        private readonly IEvaluacionPsicologicaRepository _evaluacionPsicologicaRepository;
        private readonly IAulaRepository _aulaRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IMapper _mapper;

        public ResultadosPsicologicosAulaHandler(
            IEvaluacionPsicologicaRepository evaluacionPsicologicaRepository,
            IAulaRepository aulaRepository,
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IMapper mapper
            )
        {
            _evaluacionPsicologicaRepository = evaluacionPsicologicaRepository;
            _aulaRepository = aulaRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _mapper = mapper;
        }
        public async Task<Response<RespuestasPsicologicasAulaDto>> Handle(ResultadosPsicologicosAulaQuery request, CancellationToken cancellationToken)
        {
            var evaPsiAula = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaPorAulaIdYUnidadId(request.AulaId, request.UnidadId) ?? throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaAula));

            ValidarValoresDeAtributos(evaPsiAula, request);

            var resultadosEvaPsiEstudiantes = ResultadosPsicologicosEstudiantes(request.UnidadId, request.AulaId, request.DimensionId);

            var resultadosPsicologicosAula = new RespuestasPsicologicasAulaDto()
            {
                RespuestasEstudiantesDto = _mapper.Map<IList<RespuestasPsicologicasEstudianteDto>>(resultadosEvaPsiEstudiantes),
            };

            return new Response<RespuestasPsicologicasAulaDto>(resultadosPsicologicosAula);
        }

        private void ValidarValoresDeAtributos(EvaluacionPsicologicaAula evaPsiAula, ResultadosPsicologicosAulaQuery request)
        {
            if (evaPsiAula?.EvaluacionPsicologica == null) throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologica));

            if (evaPsiAula?.EvaluacionPsicologica.Id == 2 && request.DimensionId == 1) request.DimensionId = 3;
            else if (evaPsiAula?.EvaluacionPsicologica.Id == 2 && request.DimensionId == 2) request.DimensionId = 4;
        }
        public async Task<IList<RespuestasPsicologicasEstudianteDto>> ResultadosPsicologicosEstudiantes(int unidadId, int aulaId, int dimensionId)
        {
            var evaluacionPsicologicaAula = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaIncluyendoListaEstudiante(aulaId, unidadId) ?? throw new EntidadNoEncontradaException(nameof(Domain.Entities.EvaluacionPsicologicaAula));

            if (evaluacionPsicologicaAula?.EvaluacionesPsicologicasEstudiante == null || evaluacionPsicologicaAula.EvaluacionesPsicologicasEstudiante.Count <= 0)
                throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaEstudiante));

            var resultadosPsicologicosEstudiantes = new List<RespuestasPsicologicasEstudianteDto>();

            foreach (var evaluacion in evaluacionPsicologicaAula.EvaluacionesPsicologicasEstudiante)
            {
                if (evaluacion.Estudiante != null && evaluacion.Estudiante.Persona != null)
                {
                    var estudianteDto = _mapper.Map<EstudianteDto>(evaluacion.Estudiante);
                    var resultadosEstudiante = await ResultadosPsicologicosEstudiante(estudianteDto.Id, evaluacionPsicologicaAula.EvaluacionPsicologicaId, unidadId, aulaId, dimensionId);
                    resultadosPsicologicosEstudiantes.Add(resultadosEstudiante); // tener cuidado con el null
                }
            }
            return resultadosPsicologicosEstudiantes;
        }
        public async Task<RespuestasPsicologicasEstudianteDto> ResultadosPsicologicosEstudiante(int estudianteId, int evaPsiId, int unidadId, int aulaId, int dimensionId)
        {
            var evaPsiEst = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudiante(estudianteId, unidadId, aulaId) ?? throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaEstudiante));

            var respuestasEscalasPsicologicas = await _evaluacionPsicologicaRepository.ResultadosPsicologicosEstudiante(evaPsiEst.Id, evaPsiId, dimensionId);

            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(respuestasEscalasPsicologicas);
            var respuestasEstudianteDto = new RespuestasPsicologicasEstudianteDto { EscalasPsicologicas = escalasPsicologicasDto };

            CalcularResultadosEvaPsiEstudiante(respuestasEstudianteDto);
            return respuestasEstudianteDto;
        }

        private static void CalcularResultadosEvaPsiEstudiante(RespuestasPsicologicasEstudianteDto respuestasEstudianteDto)
        {
            if (respuestasEstudianteDto.EscalasPsicologicas == null || respuestasEstudianteDto.EscalasPsicologicas.Any() is false) return;

            foreach (var escala in respuestasEstudianteDto.EscalasPsicologicas)
            {
                double? promedioEscala = null;

                if (escala.IndicadoresPsicologicos != null && escala.IndicadoresPsicologicos.Any())
                {
                    foreach (var indicador in escala.IndicadoresPsicologicos)
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
                            CategorizarIndicadorPsicologica(respuestasEstudianteDto, indicador.PromedioIndicador ?? 0.0);
                            indicador.PreguntasPsicologicas = null;
                        }
                    }

                    var totalPromedioIndicadores = escala.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioEscala = Math.Round(totalPromedioIndicadores / escala.IndicadoresPsicologicos.Count, 4);

                }
                escala.PromedioEscala = promedioEscala;
                CategorizarEscalaPsicologica(respuestasEstudianteDto, promedioEscala ?? 0.0);
            }
            var totalPromedioEscalas = respuestasEstudianteDto.EscalasPsicologicas.Sum(i => i.PromedioEscala ?? 0.0);
            respuestasEstudianteDto.PromedioEvaluacionPsicologica = Math.Round(totalPromedioEscalas / respuestasEstudianteDto.EscalasPsicologicas.Count, 4);

            respuestasEstudianteDto.EscalasPsicologicas = null; // Como es una evalucion por aula, no se necesita tener en memoria las escalas psicologicas del 
        }
        private static void CategorizarIndicadorPsicologica(RespuestasPsicologicasEstudianteDto respuestasEstudianteDto, double promedioIndicador)
        {
            if (promedioIndicador <= 1) respuestasEstudianteDto.TotalIndicadoresEnInicio++;
            else if (promedioIndicador > 1 && promedioIndicador < 3) respuestasEstudianteDto.TotalIndicadoresEnProceso++;
            else if (promedioIndicador >= 3) respuestasEstudianteDto.TotalIndicadoresSatisfactorio++;
        }

        private static void CategorizarEscalaPsicologica(RespuestasPsicologicasEstudianteDto respuestasEstudianteDto, double promedioEscala)
        {
            if (promedioEscala <= 1) respuestasEstudianteDto.TotalEscalasEnInicio++;
            else if (promedioEscala > 1 && promedioEscala < 3) respuestasEstudianteDto.TotalEscalasEnProceso++;
            else if (promedioEscala >= 3) respuestasEstudianteDto.TotalEscalasSatisfactorio++;
        }




    }

}


