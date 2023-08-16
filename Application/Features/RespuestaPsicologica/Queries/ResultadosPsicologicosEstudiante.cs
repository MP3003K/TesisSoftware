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
    public class ResultadosPsicologicosEstudiantesQuery: IRequest<Response<RespuestasEstudianteDto>>
    {
        public int EstudianteId { get; set; }
        public int DimensionId { get; set; }
        public int UnidadId { get; set; }
    }
    public class ResultadosPsicologicosEstudiantesHandler: IRequestHandler<ResultadosPsicologicosEstudiantesQuery, Response<RespuestasEstudianteDto>>
    {
        private readonly IEstudianteAulaRepository _estudianteAulaRepository;
        private readonly IAulaRepository _aulaRepository;
        private readonly IGradoEvaPsicologicaRepository _gradoEvaPsicologicaRepository;
        private readonly IIndicadorPsicologicoRepository _indicadorPsicologicoRepository;
        private readonly IEscalaPsicologicaRepository _escalaPsicologicaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IUnidadRepository _unidadRepository; 
        private readonly IMapper _mapper;

        public ResultadosPsicologicosEstudiantesHandler(
            IEstudianteAulaRepository estudianteAulaRepository,
            IAulaRepository aulaRepository,
            IGradoEvaPsicologicaRepository gradoEvaPsicologicaRepository,
            IIndicadorPsicologicoRepository indicadorPsicologicoRepository,
            IEscalaPsicologicaRepository escalaPsicologicaRepository,
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IUnidadRepository unidadRepository,
            IMapper mapper)
        {
            _estudianteAulaRepository = estudianteAulaRepository;
            _aulaRepository = aulaRepository;
            _gradoEvaPsicologicaRepository = gradoEvaPsicologicaRepository;
            _indicadorPsicologicoRepository = indicadorPsicologicoRepository;
            _escalaPsicologicaRepository = escalaPsicologicaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _unidadRepository = unidadRepository;
            _mapper = mapper;
        }
        public async Task<Response<RespuestasEstudianteDto>> Handle(ResultadosPsicologicosEstudiantesQuery request, CancellationToken cancellationToken)
        {
            var unidadId = request.UnidadId;
            var estudianteId = request.EstudianteId;
            var dimensionId = request.DimensionId;

            var unidad = await _unidadRepository.GetByIdAsync(unidadId);
            // Encontrar AulaId del estudiante
            var aula = await _estudianteAulaRepository.AulaPorEstudianteIdYAnio(estudianteId, unidad.Año);
            // Encontrar Evalucion Psicologica del Aula
            var evaPsiAula = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaPorAulaIdYUnidadId(aula.Id, unidad.Id);
            var evaPsiEstId = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudianteIdPorEstudianteId((int)evaPsiAula.Id, estudianteId);

            var evaPsiId = evaPsiAula.EvaluacionPsicologicaId;

            var escalasPsicologicas = await _escalaPsicologicaRepository.ObtenerEscalaPorDimensionId((int)evaPsiId, dimensionId);
            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(escalasPsicologicas);
            var respuestasEstudianteDto = new RespuestasEstudianteDto();
            respuestasEstudianteDto.EscalasPsicologicas = escalasPsicologicasDto;

            // Funcion para poder guardar los resultados en las escalas
            int countIndicadoresEnInicio = 0, countIndicadoresEnProceso = 0, countIndicadoresSatisfactorio = 0;
            int countEscalasEnInicio = 0, countEscalasEnProceso = 0, countEscalasSatisfactorio = 0;

            foreach (var escalaDto in respuestasEstudianteDto.EscalasPsicologicas)
            {
                double? promedioEscala = null;

                if (escalaDto.IndicadoresPsicologicos != null && escalaDto.IndicadoresPsicologicos.Any())
                {
                    foreach (var indicador in escalaDto.IndicadoresPsicologicos)
                    {
                        indicador.PromedioIndicador = await _indicadorPsicologicoRepository.PromedioIndicadorPsicologicoEstudiante((int)evaPsiEstId, indicador.Id);
                        // Contar indicadores en cada categoría
                        if (indicador.PromedioIndicador <= 1)
                            countIndicadoresEnInicio++;
                        else if (indicador.PromedioIndicador > 1 && indicador.PromedioIndicador < 3)
                            countIndicadoresEnProceso++;
                        else if (indicador.PromedioIndicador >= 3)
                            countIndicadoresSatisfactorio++;
                    }
                    var totalPromedioIndicadores = escalaDto.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioEscala = Math.Round(totalPromedioIndicadores / escalaDto.IndicadoresPsicologicos.Count, 4);
                    // Contar escalas en cada categoría
                    if (promedioEscala <= 1)
                        countEscalasEnInicio++;
                    else if (promedioEscala > 1 && promedioEscala < 3)
                        countEscalasEnProceso++;
                    else if (promedioEscala >= 3)
                        countEscalasSatisfactorio++;
                }
                escalaDto.PromedioEscala = promedioEscala;
            }
            respuestasEstudianteDto.TotalEscalasEnInicio = countEscalasEnInicio;
            respuestasEstudianteDto.TotalEscalasEnProceso = countEscalasEnProceso;
            respuestasEstudianteDto.TotalEscalasSatisfactorio = countEscalasSatisfactorio;
            respuestasEstudianteDto.TotalIndicadoresEnInicio = countIndicadoresEnInicio;
            respuestasEstudianteDto.TotalIndicadoresEnProceso = countIndicadoresEnProceso;
            respuestasEstudianteDto.TotalIndicadoresSatisfactorio = countIndicadoresSatisfactorio;

            return new Response<RespuestasEstudianteDto>(respuestasEstudianteDto);
        }
    }
}
