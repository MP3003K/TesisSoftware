﻿using Application.Exceptions;
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
    public class ResultadosPsicologicosEstudiantesQuery: IRequest<Response<RespuestasEstudianteDto>>
    {
        public int EstudianteId { get; set; }
        public int UnidadId { get; set; }
        public int AulaId { get; set; }
        public int DimensionId { get; set; }

    }
    public class ResultadosPsicologicosEstudiantesHandler: IRequestHandler<ResultadosPsicologicosEstudiantesQuery, Response<RespuestasEstudianteDto>>
    {
        private readonly IEvaluacionPsicologicaRepository _evaluacionPsicologicaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IMapper _mapper;
        
        public ResultadosPsicologicosEstudiantesHandler(
            IEvaluacionPsicologicaRepository evaluacionPsicologicaRepository,
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaRepository = evaluacionPsicologicaRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _mapper = mapper;
        }
        public async Task<Response<RespuestasEstudianteDto>> Handle(ResultadosPsicologicosEstudiantesQuery request, CancellationToken cancellationToken)
        {
            var evaPsiEst = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudiante(request.EstudianteId, request.UnidadId, request.AulaId) ?? throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaEstudiante));
         
            ValidarValoresDeAtributos(evaPsiEst, request);

            var respuestasEscalasPsicologicas = await _evaluacionPsicologicaRepository.ResultadosPsicologicosEstudiante(evaPsiEst.Id, evaPsiEst!.EvaluacionAula!.EvaluacionPsicologica!.Id, request.DimensionId) ?? throw new EntidadNoPuedeSerEliminadaPorSusDependenciasException(nameof(EvaluacionPsicologicaEstudiante));

            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(respuestasEscalasPsicologicas);
            var respuestasEstudianteDto = new RespuestasEstudianteDto{EscalasPsicologicas = escalasPsicologicasDto};

            CalcularResultadosEvaluacionPsicologica(respuestasEstudianteDto);

            return new Response<RespuestasEstudianteDto>(respuestasEstudianteDto);
        }

        private static void ValidarValoresDeAtributos(EvaluacionPsicologicaEstudiante evaPsiEst, ResultadosPsicologicosEstudiantesQuery request)
        {
            if(evaPsiEst.EvaluacionAula == null) throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaAula));

            if (evaPsiEst.EvaluacionAula.EvaluacionPsicologica == null) throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologica));

            if (evaPsiEst.EvaluacionAula.EvaluacionPsicologica.Id == 2 && request.DimensionId == 1)
                request.DimensionId = 3;
            else if (evaPsiEst.EvaluacionAula.EvaluacionPsicologica.Id == 2 && request.DimensionId == 2)
                request.DimensionId = 4;
        }

        private static void CalcularResultadosEvaluacionPsicologica(RespuestasEstudianteDto respuestasEstudianteDto)
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
        private static void CategorizarIndicadorPsicologica(RespuestasEstudianteDto respuestasEstudianteDto, double promedioIndicador)
        {
            if (promedioIndicador <= 1) respuestasEstudianteDto.TotalIndicadoresEnInicio++;
            else if (promedioIndicador > 1 && promedioIndicador < 3) respuestasEstudianteDto.TotalIndicadoresEnProceso++;
            else if (promedioIndicador >= 3) respuestasEstudianteDto.TotalIndicadoresSatisfactorio++;
        }

        private static void CategorizarEscalaPsicologica(RespuestasEstudianteDto respuestasEstudianteDto, double promedioEscala)
        {
            if (promedioEscala <= 1) respuestasEstudianteDto.TotalEscalasEnInicio++;
            else if (promedioEscala > 1 && promedioEscala < 3) respuestasEstudianteDto.TotalEscalasEnProceso++;
            else if (promedioEscala >= 3) respuestasEstudianteDto.TotalEscalasSatisfactorio++;
        }


    }
}
