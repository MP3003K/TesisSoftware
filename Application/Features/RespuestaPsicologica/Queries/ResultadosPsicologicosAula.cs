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
        private readonly IEstudianteAulaRepository _estudianteAulaRepository;
        private readonly IAulaRepository _aulaRepository;
        private readonly IGradoEvaPsicologicaRepository _gradoEvaPsicologicaRepository;
        private readonly IIndicadorPsicologicoRepository _indicadorPsicologicoRepository;
        private readonly IEscalaPsicologicaRepository _escalaPsicologicaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IUnidadRepository _unidadRepository; 
        private readonly IRespuestaPsicologicaRepository _respuestaPsicologicaRepository;
        private readonly IMapper _mapper;

        public ResultadosPsicologicosAulaHandler(
            IAulaRepository aulaRepository,
            IEscalaPsicologicaRepository escalaPsicologicaRepository,
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IUnidadRepository unidadRepository,
            IRespuestaPsicologicaRepository respuestaPsicologicaRepository,
            IMapper mapper)
        {            
            _aulaRepository = aulaRepository;
            _escalaPsicologicaRepository = escalaPsicologicaRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _unidadRepository = unidadRepository;
            _respuestaPsicologicaRepository = respuestaPsicologicaRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<EscalaPsicologicaDto>>> Handle(ResultadosPsicologicosAulaQuery request, CancellationToken cancellationToken)
        {
            var aulaId = 1;
            var dimensionId = 1;
            var unidadId = 4;

            // Obtener Unidad
            var unidad = await _unidadRepository.GetByIdAsync(unidadId);
            // Obtener Aula
            var aula = await _aulaRepository.GetByIdAsync(aulaId);
            // Obtener EvalucionPsicologicasAula por AulaId y UnidadId
            var evaPsiAula = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaPorAulaIdYUnidadId(aula.Id, unidad.Id);

            // Obtener EscalasPsicologicas por EvaPsiId y DimensionId
            var escalasPsicologicas = await _escalaPsicologicaRepository.ObtenerEscalaPorDimensionId((int)evaPsiAula.EvaluacionPsicologicaId, dimensionId);
            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(escalasPsicologicas);


            foreach (var escalaDto in escalasPsicologicasDto)
            {
                double? promedioEscala = null;

                if (escalaDto.IndicadoresPsicologicos != null && escalaDto.IndicadoresPsicologicos.Any())
                {
                    foreach (var indicador in escalaDto.IndicadoresPsicologicos)
                        indicador.PromedioIndicador = await _respuestaPsicologicaRepository.PromedioRespuestasIndicadorEnAula((int)evaPsiAula.Id, indicador.Id);
                    
                    var totalPromedioIndicadores = escalaDto.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioEscala = Math.Round(totalPromedioIndicadores / escalaDto.IndicadoresPsicologicos.Count, 2);
                }
                escalaDto.PromedioEscala = promedioEscala;
            }
            Console.WriteLine(escalasPsicologicasDto);

            return new Response<IList<EscalaPsicologicaDto>>(escalasPsicologicasDto);
        }
    }
}
