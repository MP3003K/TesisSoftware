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
    public class ListaResultadosEstudianteQuery: IRequest<Response<IList<ResultadosPsicologicosEstudianteDto>>>
    {
    }
    public class ListaResultadosEstudianteHandler : IRequestHandler<ListaResultadosEstudianteQuery, Response<IList<ResultadosPsicologicosEstudianteDto>>>
    {
        private readonly IEstudianteAulaRepository _estudianteAulaRepository;
        private readonly IAulaRepository _aulaRepository;
        private readonly IGradoEvaPsicologicaRepository _gradoEvaPsicologicaRepository;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IMapper _mapper;
        public ListaResultadosEstudianteHandler(
            IEstudianteAulaRepository estudianteAulaRepository,
            IAulaRepository aulaRepository,
            IGradoEvaPsicologicaRepository gradoEvaPsicologicaRepository,
            IUnidadRepository unidadRepository,
            IMapper mapper)
        {
            _estudianteAulaRepository = estudianteAulaRepository;
            _aulaRepository = aulaRepository;
            _gradoEvaPsicologicaRepository = gradoEvaPsicologicaRepository;
            _unidadRepository = unidadRepository;
            _mapper = mapper;
        }


        public async Task<Response<IList<ResultadosPsicologicosEstudianteDto>>> Handle(ListaResultadosEstudianteQuery request, CancellationToken cancellationToken)
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

            throw new NotImplementedException();
        }
    }
}
