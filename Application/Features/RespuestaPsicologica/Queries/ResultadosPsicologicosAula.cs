using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
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
    public class ResultadosPsicologicosAulaQuery: IRequest<Response<IList<EscalaPsicologicaDto>>>
    {
        public int AulaId { get; set; }
        public int DimensionId { get; set; }
        public int UnidadId { get; set; }
    }
    public class ResultadosPsicologicosAulaHandler: IRequestHandler<ResultadosPsicologicosAulaQuery, Response<IList<EscalaPsicologicaDto>>>
    {
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IEvaluacionPsicologicaRepository _evaluacionPsicologicaRepository;
        private readonly IMapper _mapper;

        public ResultadosPsicologicosAulaHandler(
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IEvaluacionPsicologicaRepository evaluacionPsicologicaRepository,
            IMapper mapper
            )
        {            
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _evaluacionPsicologicaRepository = evaluacionPsicologicaRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<EscalaPsicologicaDto>>> Handle(ResultadosPsicologicosAulaQuery request, CancellationToken cancellationToken)
        {
            var evaPsiAula = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaPorAulaIdYUnidadId(request.AulaId, request.UnidadId) ?? throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaAula));

            ValidarValoresDeAtributos(evaPsiAula, request);

            var respuestasEscalasPsicologicas = await _evaluacionPsicologicaRepository.ResultadosPsicologicosAula(evaPsiAula.Id, evaPsiAula.EvaluacionPsicologicaId, request.DimensionId);
            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(respuestasEscalasPsicologicas);

            CalcularResultadosEvaluacionPsicologica(escalasPsicologicasDto);

            return new Response<IList<EscalaPsicologicaDto>>(escalasPsicologicasDto);
        }

        private void ValidarValoresDeAtributos(EvaluacionPsicologicaAula evaPsiAula, ResultadosPsicologicosAulaQuery request)
        {
            if (evaPsiAula?.EvaluacionPsicologica == null) throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologica));

            if (evaPsiAula?.EvaluacionPsicologica.Id == 2 && request!.DimensionId == 1) request.DimensionId = 3;
            else if (evaPsiAula?.EvaluacionPsicologica.Id == 2 && request!.DimensionId == 2) request.DimensionId = 4;
        }

        private static void CalcularResultadosEvaluacionPsicologica(IList<EscalaPsicologicaDto> escalasPsicologicasDto)
        {
            foreach (var escalaDto in escalasPsicologicasDto)
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
        }
    }
}


