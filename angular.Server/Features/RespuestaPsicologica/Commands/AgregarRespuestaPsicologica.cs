using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using DTOs;
using MediatR;
using System.Text.RegularExpressions;
using webapi.Dao.Repositories;

namespace Application.Features.RespuestaPsicologica.Commands
{
    public class AgregarRespuestaPsicologicaRequest : IRequest<Response<RespuestaPsicologicaDto>>
    {
        public string? Respuesta { get; set; }
        public int? EvaPsiEstId { get; set; }
        public int? PreguntaPsicologicaId { get; set; }
    }

    public class AgregarRespuestaPsicologicaHandler : IRequestHandler<AgregarRespuestaPsicologicaRequest, Response<RespuestaPsicologicaDto>>
    {
        private readonly IRespuestaPsicologicaRepository _respuestaPsicologicaRepository;
        private readonly IMapper _mapper;

        public AgregarRespuestaPsicologicaHandler(
            IRespuestaPsicologicaRepository respuestaPsicologicaRepository,
            IMapper mapper)
        {
            _respuestaPsicologicaRepository = respuestaPsicologicaRepository;
            _mapper = mapper;
        }

        public async Task<Response<RespuestaPsicologicaDto>> Handle(
            AgregarRespuestaPsicologicaRequest request,
            CancellationToken cancellationToken)
        {

            // Validar Null
            if (string.IsNullOrEmpty(request.Respuesta) || !Regex.IsMatch(request.Respuesta, @"^[0-9]+$"))
                throw new AtributoNoPuedeSerNullException(nameof(request.Respuesta));
            if (request.EvaPsiEstId is null or <= 0)
                throw new AtributoNoPuedeSerNullException(nameof(request.EvaPsiEstId));
            if (request.PreguntaPsicologicaId is null or <= 0)
                throw new AtributoNoPuedeSerNullException(nameof(request.PreguntaPsicologicaId));
            
            // Verificar si la pregunta ya teniene respuesta
            var respuestaPsicologica = await _respuestaPsicologicaRepository
                .GetRespuestaPsicologica((int)request.EvaPsiEstId, (int)request.PreguntaPsicologicaId);

            if(respuestaPsicologica != null)
            {
                // Actualizar Respuesta Psicologica
                respuestaPsicologica.UpdateRespuesta(request.Respuesta);
                await _respuestaPsicologicaRepository.UpdateAsync(respuestaPsicologica);
            }
            else
            {
                // Crear Respuesta Psicologica
                respuestaPsicologica = new Domain.Entities.RespuestaPsicologica(request.Respuesta, (int)request.PreguntaPsicologicaId,  (int)request.EvaPsiEstId);
                await _respuestaPsicologicaRepository.InsertAsync(respuestaPsicologica);
            }
            var respuestaPsicologicaDto = _mapper.Map<RespuestaPsicologicaDto>(respuestaPsicologica);

            return new Response<RespuestaPsicologicaDto>(respuestaPsicologicaDto);
        }
    }
}
