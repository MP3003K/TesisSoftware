using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Entities;
using DTOs;
using MediatR;
using Interfaces.Repositories;

namespace Application.Features.RespuestaPsicologica.Commands
{
    public class ActualizarEstadoEvaPsiEstRequest : IRequest<Response<EvaluacionPsicologicaEstudianteDto>>
    {
        public int? EvaPsiEstId { get; set; }
        public string? Estado { get; set; }
    }
    public class ActualizarEstadoEvaPsiEstHandler : IRequestHandler<ActualizarEstadoEvaPsiEstRequest, Response<EvaluacionPsicologicaEstudianteDto>>
    {
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IMapper _mapper;

        public ActualizarEstadoEvaPsiEstHandler(
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _mapper = mapper;
        }
        public async Task<Response<EvaluacionPsicologicaEstudianteDto>> Handle(
            ActualizarEstadoEvaPsiEstRequest request,
            CancellationToken cancellationToken)
        {
            // Validaciones Null
            if (request.EvaPsiEstId is null || request.EvaPsiEstId <= 0) throw new AtributoNecesitaSerMayorACeroException(nameof(request.EvaPsiEstId));
            if (string.IsNullOrEmpty(request.Estado)) throw new AtributoNoPuedeSerNullException(nameof(request.Estado));
            // Obtener Evaluacion Psicologica Estudiante
            var evaPsiEst = await _evaluacionPsicologicaEstudianteRepository.GetByIdAsync((int)request.EvaPsiEstId) ?? throw new EntidadNoEncontradaException(nameof(EvaluacionPsicologicaEstudiante));
            // Actualizar Estado de la Evalucion Psicologica Estudiante
            evaPsiEst.UpdateEstado(request.Estado);
            await _evaluacionPsicologicaEstudianteRepository.UpdateAsync(evaPsiEst);
            var evaPsiEstDto = _mapper.Map<EvaluacionPsicologicaEstudianteDto>(evaPsiEst);

            return new Response<EvaluacionPsicologicaEstudianteDto>(evaPsiEstDto);
        }

    }
}
