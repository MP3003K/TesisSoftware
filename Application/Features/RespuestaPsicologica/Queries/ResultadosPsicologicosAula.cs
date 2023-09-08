using Application.Exceptions;
using Application.Features.Aula.Queries;
using Application.Wrappers;
using AutoMapper;
using Azure.Core;
using Contracts.Repositories;
using Domain.Entities;
using DTOs;
using MediatR;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RespuestaPsicologica.Queries
{
    public class ResultadosPsicologicosAulaQuery: IRequest<Response<RespuestasPsicologicasAulaDto>>
    {
        public int AulaId { get; set; }
        public int DimensionId { get; set; }
        public int UnidadId { get; set; }
    }
    public class ResultadosPsicologicosAulaHandler: IRequestHandler<ResultadosPsicologicosAulaQuery, Response<RespuestasPsicologicasAulaDto>>
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

            var respuestasEscalasPsicologicas = await _evaluacionPsicologicaRepository.ResultadosPsicologicosAula(evaPsiAula.Id, evaPsiAula.EvaluacionPsicologicaId, request.DimensionId) ?? throw new EntidadNoEncontradaException(nameof(Domain.Entities.RespuestaPsicologica));
            if (respuestasEscalasPsicologicas.Any() is false) throw new EntidadNoEncontradaException(nameof(Domain.Entities.RespuestaPsicologica));

            var resultadosEvaPsiEstudiantes = ResultadosPsicologicosEstudiantes(request.UnidadId, request.AulaId, request.DimensionId);
            if(respuestasEscalasPsicologicas == null || respuestasEscalasPsicologicas.Any() is false) throw new EntidadNoEncontradaException(nameof(Domain.Entities.RespuestaPsicologica));

            var escalasPsicologicas = escalasPsicologicasDto.Where(esc => esc.IndicadoresPsicologicos != null && esc.IndicadoresPsicologicos.Any()).ToList();
            var resultadosPsicologicosAula = new RespuestasPsicologicasAulaDto();

            ResultadosPsicologicosAula(resultadosEvaPsiEstudiantes.Result);


            return null;
        }

        private void ResultadosPsicologicosAula(IList<RespuestasPsicologicasEstudianteDto> resultadosEvaPsiEstudiantes)
        {
            var resultadosPsicologicosAula = new RespuestasPsicologicasAulaDto();
            double cantEscalas = 0, cantIndicadores = 0;
            double sumaEscalas = 0, sumaIndicadores = 0;

            foreach(var respuestaEstudiante in resultadosEvaPsiEstudiantes)
            {
                if (respuestaEstudiante.EscalasPsicologicas is null) break;

                foreach(var escalas in respuestaEstudiante.EscalasPsicologicas)
                {
                    if(escalas.IndicadoresPsicologicos is null) break;
                    foreach(var indicador in escalas.IndicadoresPsicologicos)
                    {
                        if (indicador.PromedioIndicador != null)
                        {
                            sumaIndicadores += indicador.PromedioIndicador.Value;
                            cantIndicadores++;
                        }
                    }

                    if (escalas.PromedioEscala != null)
                    {
                        sumaEscalas += escalas.PromedioEscala.Value;
                        cantEscalas++;
                    }

                }
            }

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
                    var resultadosEstudiante = await ResultadosPsicologicosEstudiante(estudianteDto.Id, unidadId, aulaId, dimensionId);
                    resultadosPsicologicosEstudiantes.Add(resultadosEstudiante); // tener cuidado con el null
                }
            }
            return resultadosPsicologicosEstudiantes;
        }
        public async Task<RespuestasPsicologicasEstudianteDto?> ResultadosPsicologicosEstudiante(int estudianteId, int unidadId, int aulaId, int dimensionId)
        {
            var evaPsiEst = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudiante(estudianteId, unidadId, aulaId) ?? throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaEstudiante));

            var respuestasEscalasPsicologicas = await _evaluacionPsicologicaRepository.ResultadosPsicologicosEstudiante(evaPsiEst.Id, evaPsiEst!.EvaluacionAula!.EvaluacionPsicologica!.Id, dimensionId);
            if (respuestasEscalasPsicologicas == null || respuestasEscalasPsicologicas.Any() is false) return null;
            
            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(respuestasEscalasPsicologicas);
            var respuestasEstudianteDto = new RespuestasPsicologicasEstudianteDto { EscalasPsicologicas = escalasPsicologicasDto };

            CalcularResultadosEvaPsiEstudiante(respuestasEstudianteDto);

            return respuestasEstudianteDto;
        }

        private static void CalcularResultadosEvaPsiEstudiante(RespuestasPsicologicasEstudianteDto respuestasEstudianteDto)
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
                            CategorizarIndicadorPsicologica(respuestasEstudianteDto, indicador.PromedioIndicador ?? 0.0);
                            indicador.PreguntasPsicologicas = null;
                        }
                    }

                    var totalPromedioIndicadores = escalaDto.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioEscala = Math.Round(totalPromedioIndicadores / escalaDto.IndicadoresPsicologicos.Count, 4);

                }
                escalaDto.PromedioEscala = promedioEscala;
                CategorizarEscalaPsicologica(respuestasEstudianteDto, promedioEscala ?? 0.0);
            }
            var totalPromedioEscalas = respuestasEstudianteDto.EscalasPsicologicas.Sum(i => i.PromedioEscala ?? 0.0);
            respuestasEstudianteDto.PromedioEvaluacionPsicologica = Math.Round(totalPromedioEscalas / respuestasEstudianteDto.EscalasPsicologicas.Count, 4);
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


