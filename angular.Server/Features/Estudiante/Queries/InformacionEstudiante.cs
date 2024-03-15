using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using DTOs;
using Interfaces.Repositories;
using MediatR;


namespace Application.Features.Estudiante.Queries
{
    public class InformacionEstudianteQuery : IRequest<Response<EstudianteDto>>
    {
        public int PersonaId { get; set; }
    }
    public class InformacionEstudianteHandler : IRequestHandler<InformacionEstudianteQuery, Response<EstudianteDto>>
    {
        private readonly IUnidadRepository _unidadRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IMapper _mapper;
        public InformacionEstudianteHandler(
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IPersonaRepository personaRepository,
            IUnidadRepository unidadRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _personaRepository = personaRepository;
            _unidadRepository = unidadRepository;
            _mapper = mapper;
        }
        public async Task<Response<EstudianteDto>> Handle(InformacionEstudianteQuery request, CancellationToken cancellationToken)
        {
            var estudiante = await _personaRepository.ObtenerEstudiantePorPersonaId(request.PersonaId) ?? throw new EntidadNoEncontradaException(nameof(Estudiante));
            var aula = estudiante?.EstudiantesAulas?[0].Aula ?? throw new EntidadNoEncontradaException(nameof(Aula));

            var unidadActual = await _unidadRepository.UnidadActual() ?? throw new EntidadNoEncontradaException(nameof(Unidad));

            var estudianteDto = _mapper.Map<EstudianteDto>(estudiante);

            var evaPsiAula = await _evaluacionPsicologicaAulaRepository.EvaPsiAulaPorAulaIdYUnidadId(aula.Id, unidadActual.Id);
            if(evaPsiAula != null && evaPsiAula.Id > 0)
            {
                estudianteDto.CantPregAResponder = evaPsiAula?.EvaluacionPsicologica?.CantPreguntas;
                var evaPsiEstId = await _evaluacionPsicologicaEstudianteRepository.EvaPsiEstudianteIdPorEstudianteId(evaPsiAula!.Id, estudiante.Id);
                if (evaPsiEstId > 0) estudianteDto.EvaPsiEstId = evaPsiEstId;
            }

            estudianteDto.AulaDto = _mapper.Map<AulaDto>(aula);
            estudianteDto.UnidadDto = _mapper.Map<UnidadDto>(unidadActual);

            return new Response<EstudianteDto>(estudianteDto);

        }
    }
}