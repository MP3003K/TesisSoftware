using AutoMapper;
using Azure;
using Contracts.Repositories;
using Domain.Entities;
using DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RespuestaPsicologica.Queries
{
    public class ResultadosEstudianteQuery: IRequest<Response<IList<EscalaPsicologicaDto>>>
    {
    }
    public class ListaResultadosEstudianteHandler : IRequestHandler<ResultadosEstudianteQuery, Response<IList<EscalaPsicologicaDto>>>
    {
        private readonly IEstudianteAulaRepository _estudianteAulaRepository;
        private readonly IAulaRepository _aulaRepository;
        private readonly IGradoEvaPsicologicaRepository _gradoEvaPsicologicaRepository;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IIndicadorPsicologicoRepository _indicadorPsicologicoRepository;
        private readonly IEscalaPsicologicaRepository _escalaPsicologicaRepository;
        private readonly IMapper _mapper;
        public ListaResultadosEstudianteHandler(
            IEstudianteAulaRepository estudianteAulaRepository,
            IAulaRepository aulaRepository,
            IGradoEvaPsicologicaRepository gradoEvaPsicologicaRepository,
            IUnidadRepository unidadRepository,
            IIndicadorPsicologicoRepository indicadorPsicologicoRepository,
            IEscalaPsicologicaRepository escalaPsicologicaRepository,
            IMapper mapper)
        {
            _estudianteAulaRepository = estudianteAulaRepository;
            _aulaRepository = aulaRepository;
            _gradoEvaPsicologicaRepository = gradoEvaPsicologicaRepository;
            _unidadRepository = unidadRepository;
            _indicadorPsicologicoRepository = indicadorPsicologicoRepository;
            _escalaPsicologicaRepository = escalaPsicologicaRepository;
            _mapper = mapper;
        }


        public async Task<Response<IList<EscalaPsicologicaDto>>> Handle(ResultadosEstudianteQuery request, CancellationToken cancellationToken)
        {
            var estudianteId = 2;

            // Encontrar AulaId del estudiante
            var aulaId = await _estudianteAulaRepository.AulaIdPorEstudianteId(estudianteId);
            // Encontrar Aula del estudiante
            var aula = await _aulaRepository.GetByIdAsync((int)aulaId);
            // Encontrar Evalucion Psicologica del Aula
            var evaPsiId = await _gradoEvaPsicologicaRepository.GetTestPsicologicoIdPorGrado(aula.GradoId);
            // Encontrar Unidad actual
            var unidadActual = await _unidadRepository.UnidadActual();


            var dimensionId = 1;
            var escalasPsicologicas = await _escalaPsicologicaRepository.ObtenerEscalaPorDimensionId((int)evaPsiId, dimensionId);
            var escalaPsicologicaDtos = _mapper.Map<IList<EscalaPsicologicaDto>>(escalasPsicologicas);
            var evaPsiEstId = 1;


            foreach (var escalaDto in escalaPsicologicaDtos)
            {
                double? promedioIndicadores = null;

                if (escalaDto.IndicadoresPsicologicos != null && escalaDto.IndicadoresPsicologicos.Any())
                {
                    foreach (var indicador in escalaDto.IndicadoresPsicologicos)
                    {
                        indicador.PromedioIndicador = await _indicadorPsicologicoRepository.PromedioIndicadorPsicologicoEstudiante(evaPsiEstId, indicador.Id);
                    }
                    var totalPromedioIndicadores = escalaDto.IndicadoresPsicologicos.Sum(i => i.PromedioIndicador ?? 0.0);
                    promedioIndicadores = totalPromedioIndicadores / escalaDto.IndicadoresPsicologicos.Count;
                }

                escalaDto.promedioIndicesPsicologicos = promedioIndicadores;
            }


            var response = new Response<IList<EscalaPsicologicaDto>>(escalaPsicologicaDtos);
            return response;
        }
    }
}
