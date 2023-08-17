using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PreguntaPsicologica.Queries
{
    public class ListaPreguntasPsicologicasQuery: IRequest<Response<IList<PreguntaPsicologicaDto>>>
    {
        public int EstudianteId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class ListaPreguntasYRespuestasPsicologicasHandler : IRequestHandler<ListaPreguntasPsicologicasQuery, Response<IList<PreguntaPsicologicaDto>>>
    {
        private readonly IPreguntaPsicologicaRepository _preguntaPsicologicaRepository;
        private readonly IRespuestaPsicologicaRepository _respuestaPsicologicaRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IEstudianteAulaRepository _estudianteAulaRepository;
        private readonly IUnidadRepository  _unidadRepository;
        private readonly IMapper _mapper;
        private readonly IGradoEvaPsicologicaRepository _gradoEvaPsicologicaRepository;
        public ListaPreguntasYRespuestasPsicologicasHandler(
                       IPreguntaPsicologicaRepository preguntaPsicologicaRepository,
                       IRespuestaPsicologicaRepository respuestaPsicologicaRepository,
                       IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
                       IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
                       IEstudianteAulaRepository estudianteAulaRepository,
                       IUnidadRepository unidadRepository,
                       IAulaRepository aulaRepository,
                       IGradoEvaPsicologicaRepository gradoEvaPsicologicaRepository,
                                  IMapper mapper)
        {
            _preguntaPsicologicaRepository = preguntaPsicologicaRepository;
            _respuestaPsicologicaRepository = respuestaPsicologicaRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _estudianteAulaRepository = estudianteAulaRepository;
            _unidadRepository = unidadRepository;
            _gradoEvaPsicologicaRepository = gradoEvaPsicologicaRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<PreguntaPsicologicaDto>>> Handle(ListaPreguntasPsicologicasQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;
            var estudianteId = request.EstudianteId;

            // Encontrar Aula del estudiante
            var aula = await _estudianteAulaRepository.AulaActualEstudiante(estudianteId);
            // Encontrar Evalucion Psicologica del Aula
            var evaPsiId = await _gradoEvaPsicologicaRepository.GetTestPsicologicoIdPorGrado(aula.GradoId);
            // Encontrar Unidad actual
            var unidadActual = await _unidadRepository.UnidadActual();
            // Encontrar evaluacion psicologica del Aula
            var evaPsiAulaId = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaIdPorAulaIdYUnidadId((int)aula.Id, unidadActual.Id);
            // Encontrar evalucion psicologica del Estudiante
            var evaPsiEstId = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudianteIdPorEstudianteId((int)evaPsiAulaId, estudianteId);
            // Buscar preguntas
            var preguntasPsicologicas = await _preguntaPsicologicaRepository.PreguntaPsicologicasPaginadas((int)evaPsiId, pageSize, pageNumber);
            var preguntasPsicologicasDto = _mapper.Map<IList<PreguntaPsicologicaDto>>(preguntasPsicologicas);         
           // Agregar respuestas 
            foreach (var pregunta in preguntasPsicologicasDto)
                pregunta.Respuesta = await _respuestaPsicologicaRepository.GetRespuestaDeUnaPreguntaPorEvaPsiEstIdYPreguntaId((int)evaPsiEstId, pregunta.Id);
            return new Response<IList<PreguntaPsicologicaDto>>(preguntasPsicologicasDto);
        }
    }
}
