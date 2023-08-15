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
        public int EvaPsiAulaId { get; set; }
        public int Año { get; set; }
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
        private readonly IMapper _mapper;

        public ResultadosPsicologicosEstudiantesHandler(
            IEstudianteAulaRepository estudianteAulaRepository,
            IAulaRepository aulaRepository,
            IGradoEvaPsicologicaRepository gradoEvaPsicologicaRepository,
            IIndicadorPsicologicoRepository indicadorPsicologicoRepository,
            IEscalaPsicologicaRepository escalaPsicologicaRepository,
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IMapper mapper)
        {
            _estudianteAulaRepository = estudianteAulaRepository;
            _aulaRepository = aulaRepository;
            _gradoEvaPsicologicaRepository = gradoEvaPsicologicaRepository;
            _indicadorPsicologicoRepository = indicadorPsicologicoRepository;
            _escalaPsicologicaRepository = escalaPsicologicaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<EscalaPsicologicaDto>>> Handle(ResultadosPsicologicosEstudiantesQuery request, CancellationToken cancellationToken)
        {
            var estudianteId = request.EstudianteId;
            var evaPsiAulaId = request.EvaPsiAulaId;
            var dimensionId = request.DimensionId;
            var año = request.Año;
            var nUnidad = request.NUnidad;

            // Encontrar AulaId del estudiante
            var aulaId = await _estudianteAulaRepository.AulaIdPorEstudianteId(estudianteId);
            // Encontrar Aula del estudiante
            var aula = await _aulaRepository.GetByIdAsync((int)aulaId);
            // Encontrar Evalucion Psicologica del Aula
            var evaPsiId = await _gradoEvaPsicologicaRepository.GetTestPsicologicoIdPorGrado(aula.GradoId);
            // Encontrar evalucion psicologica del Estudiante
            var evaPsiEstId = await _evaluacionPsicologicaEstudianteRepository.EvalucionPsicologicaEstudianteIdPorEstudianteId((int)evaPsiAulaId, estudianteId);
            //Obtener Escalas Psicologicas de una Dimension de una EvaluacionPsicologica


            var escalasPsicologicas = await _escalaPsicologicaRepository.ObtenerEscalaPorDimensionId((int)1, 1);
            var escalasPsicologicasDto = _mapper.Map<IList<EscalaPsicologicaDto>>(escalasPsicologicas);


            foreach (var escalaDto in escalasPsicologicasDto)
            {
                double? promedioIndicadores = null;

                if (escalaDto.IndicadoresPsicologicos != null && escalaDto.IndicadoresPsicologicos.Any())
                {
                    foreach (var indicador in escalaDto.IndicadoresPsicologicos)
                    {
                        indicador.PromedioIndicador = await _indicadorPsicologicoRepository.PromedioIndicadorPsicologicoEstudiante((int)evaPsiEstId, indicador.Id);
                    }
                    var totalPromedioIndicadores = escalaDto.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioIndicadores = totalPromedioIndicadores / escalaDto.IndicadoresPsicologicos.Count;
                }

                escalaDto.PromedioEscala = promedioIndicadores;
            }
            Console.WriteLine(escalasPsicologicasDto);

            return new Response<IList<EscalaPsicologicaDto>>(escalasPsicologicasDto);
        }
    }
}
