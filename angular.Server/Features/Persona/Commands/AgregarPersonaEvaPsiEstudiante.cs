using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;
using System.Text.RegularExpressions;
using Domain.Entities;

namespace Application.Features.Persona.Commands
{
    public class AgregarPersonaEvaPsiEstudianteRequest : IRequest<Response<EvaluacionPsicologicaEstudianteDto>>
    {
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int DNI { get; set; } = 0;
        public int AulaId { get; set; } = 0;
    }

    public class AgregarPersonaEvaPsiEstudianteHandler : IRequestHandler<AgregarPersonaEvaPsiEstudianteRequest, Response<EvaluacionPsicologicaEstudianteDto>>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolUsuarioRepository _rolUsuarioRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IEstudianteAulaRepository _estudianteAulaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IMapper _mapper;

        public AgregarPersonaEvaPsiEstudianteHandler(
            IPersonaRepository personaRepository,
            IUsuarioRepository usuarioRepository,
            IRolUsuarioRepository rolUsuarioRepository,
            IEstudianteRepository estudianteRepository,
            IEstudianteAulaRepository estudianteAulaRepository,
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IUnidadRepository unidadRepository,
            IMapper mapper)
        {
            _personaRepository = personaRepository;
            _usuarioRepository = usuarioRepository;
            _rolUsuarioRepository = rolUsuarioRepository;
            _estudianteRepository = estudianteRepository;
            _estudianteAulaRepository = estudianteAulaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _unidadRepository = unidadRepository;
            _mapper = mapper;
        }

        public async Task<Response<EvaluacionPsicologicaEstudianteDto>> Handle(
            AgregarPersonaEvaPsiEstudianteRequest request,
            CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Nombres))
                throw new AtributoNoPuedeSerNullException(nameof(request.Nombres));
            if (string.IsNullOrEmpty(request.ApellidoPaterno))
                throw new AtributoNoPuedeSerNullException(nameof(request.ApellidoPaterno));
            if (string.IsNullOrEmpty(request.ApellidoMaterno))
                throw new AtributoNoPuedeSerNullException(nameof(request.ApellidoMaterno));

            if (request.DNI <= 0 || !Regex.IsMatch(request.DNI.ToString(), @"^\d{8}$"))
                throw new AtributoNoPuedeSerNullException(nameof(request.DNI));

            request.Nombres = request.Nombres.Trim();
            request.ApellidoPaterno = request.ApellidoPaterno.Trim();
            request.ApellidoMaterno = request.ApellidoMaterno.Trim();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       

            // Creacion estudiante
            var persona = new Domain.Entities.Persona(request.Nombres, request.ApellidoPaterno, request.ApellidoMaterno, request.DNI);
            await _personaRepository.InsertAsync(persona);
            var usuario = new Domain.Entities.Usuario(persona.DNI.ToString(), persona.DNI.ToString(), persona.Id);
            await _usuarioRepository.InsertAsync(usuario);
            var rolUsuario = new RolUsuario(usuario.Id, 2);
            await _rolUsuarioRepository.InsertAsync(rolUsuario);
            var estudiante = new Domain.Entities.Estudiante(persona.DNI.ToString(), persona.Id);
            await _estudianteRepository.InsertAsync(estudiante);

            // Asignacion Aula

            var unidadActual = await _unidadRepository.UnidadActual() ?? throw new AtributoNoPuedeSerNullException(nameof(Domain.Entities.Aula));

            var estudianteAula = new EstudianteAula(estudiante.Id, request.AulaId);
            await _estudianteAulaRepository.InsertAsync(estudianteAula);

            var evaPsiAulaId = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaIdPorAulaIdYUnidadId(request.AulaId, unidadActual.Id) ?? throw new AtributoNoPuedeSerNullException(nameof(EvaluacionPsicologicaAula));

            var evaPsiEst = new EvaluacionPsicologicaEstudiante(evaPsiAulaId, estudiante.Id);
            await _evaluacionPsicologicaEstudianteRepository.InsertAsync(evaPsiEst);

            var evaPsiEstDto = _mapper.Map<EvaluacionPsicologicaEstudianteDto>(evaPsiEst);

            return new Response<EvaluacionPsicologicaEstudianteDto>(evaPsiEstDto);
        }
    }
}