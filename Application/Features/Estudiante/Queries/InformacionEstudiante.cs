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

namespace Application.Features.Estudiante.Queries
{
    public class InformacionEstudianteQuery : IRequest<Response<EstudianteDto>>
    {
        public int PersonaId { get; set; }
    }
    public class InformacionEstudianteHandler : IRequestHandler<InformacionEstudianteQuery, Response<EstudianteDto>>
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IEstudianteAulaRepository _estudianteAulaRepository;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IMapper _mapper;
        public InformacionEstudianteHandler(
            IEstudianteRepository estudianteRepository,
            IEstudianteAulaRepository estudianteAulaRepository,
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IPersonaRepository personaRepository,
            IUnidadRepository unidadRepository,
            IMapper mapper)
        {
            _estudianteRepository = estudianteRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _personaRepository = personaRepository;
            _estudianteAulaRepository = estudianteAulaRepository;
            _unidadRepository = unidadRepository;
            _mapper = mapper;
        }
        public async Task<Response<EstudianteDto>> Handle(InformacionEstudianteQuery request, CancellationToken cancellationToken)
        {
            var personaId = request.PersonaId;
            var estudiante = await _personaRepository.ObtenerEstudiantePorPersonaId(personaId);
            var aula = await _estudianteAulaRepository.AulaActualEstudiante(estudiante.Id);
            var unidadActual = await _unidadRepository.UnidadActual();
            // Encontrar evaluacion psicologica del Aula
            var evaPsiAulaId = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaIdPorAulaIdYUnidadId(aula.Id, unidadActual.Id);
            // Encontrar evalucion psicologica del Estudiante
            var evaPsiEstId = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudianteIdPorEstudianteId((int)evaPsiAulaId, estudiante.Id);

            var estudianteDto = _mapper.Map<EstudianteDto>(estudiante);
            estudianteDto.EvaPsiEstId = evaPsiEstId;
            estudianteDto.AulaDto = _mapper.Map<AulaDto>(aula);
            estudianteDto.UnidadDto = _mapper.Map<UnidadDto>(unidadActual);

            return new Response<EstudianteDto>(estudianteDto);

        }
    }
}