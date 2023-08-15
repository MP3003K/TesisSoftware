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
    public class ResultadosPsicologicosEstudiantesQuery: IRequest<Response<IList<EscalaPsicologicaDto>>>
    {
        public int EstudianteId { get; set; }
        public int DimensionId { get; set; }
        public int Anio { get; set; }
        public int NUnidad { get; set; }
    }
    public class ResultadosPsicologicosEstudiantesHandler: IRequestHandler<ResultadosPsicologicosEstudiantesQuery, Response<IList<EscalaPsicologicaDto>>>
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
        public async Task<Response<IList<EscalaPsicologicaDto>>> Handle(ResultadosPsicologicosEstudiantesQuery request, CancellationToken cancellationToken)
        {


            var anio = request.Anio;
            var nUnidad = request.NUnidad;
            var estudianteId = request.EstudianteId;
            var dimensionId = request.DimensionId;

            var unidad = await _unidadRepository.ObtenerUnidadPorAnioYNUnidad(anio, nUnidad);
            // Encontrar AulaId del estudiante
            var aula = await _estudianteAulaRepository.AulaPorEstudianteIdYAnio(estudianteId, anio);
            // Encontrar Evalucion Psicologica del Aula
            var evaPsiAula = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaPorAulaIdYUnidadId(aula.Id, unidad.Id);
            var evaPsiEstId = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudianteIdPorEstudianteId((int)evaPsiAula.Id, estudianteId);

            var evaPsiId = evaPsiAula.EvaluacionPsicologicaId;

            var escalasPsicologicas = await _escalaPsicologicaRepository.ObtenerEscalaPorDimensionId((int)evaPsiId, dimensionId);
            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(escalasPsicologicas);


            foreach (var escalaDto in escalasPsicologicasDto)
            {
                double? promedioEscala = null;

                if (escalaDto.IndicadoresPsicologicos != null && escalaDto.IndicadoresPsicologicos.Any())
                {
                    foreach (var indicador in escalaDto.IndicadoresPsicologicos)
                    {
                        indicador.PromedioIndicador = await _indicadorPsicologicoRepository.PromedioIndicadorPsicologicoEstudiante((int)evaPsiEstId, indicador.Id);
                    }
                    var totalPromedioIndicadores = escalaDto.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioEscala = Math.Round(totalPromedioIndicadores / escalaDto.IndicadoresPsicologicos.Count, 4);
                }

                escalaDto.PromedioEscala = promedioEscala;
            }
            Console.WriteLine(escalasPsicologicasDto);

            return new Response<IList<EscalaPsicologicaDto>>(escalasPsicologicasDto);
        }
    }
}
