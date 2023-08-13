using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.RespuestaPsicologica.Queries;

public class ListaRespuestasPsicologicasQuery : IRequest<Response<IList<RespuestaPsicologicaDto>>>
{
    public int EvaPsiEstId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class ListaRespuestasPsicologicasHandler : IRequestHandler<ListaRespuestasPsicologicasQuery, Response<IList<RespuestaPsicologicaDto>>>
{
    private readonly IRespuestaPsicologicaRepository _respuestaPsicologicaRepository;
    private readonly IMapper _mapper;

    public ListaRespuestasPsicologicasHandler(
        IRespuestaPsicologicaRepository respuestaPsicologicaRepository,
        IMapper mapper)
    {
        _respuestaPsicologicaRepository = respuestaPsicologicaRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<RespuestaPsicologicaDto>>> Handle(ListaRespuestasPsicologicasQuery request, CancellationToken cancellationToken)
    {
        var evaPsiEstId = request.EvaPsiEstId;
        var pageNumber = request.PageNumber;
        var pageSize = request.PageSize;

        var respuestasPsicologicas = await _respuestaPsicologicaRepository.ObtenerRespuestasDeEstudiante(evaPsiEstId, pageNumber, pageSize);

        var respuestasPsicologicasDto = _mapper.Map<IList<RespuestaPsicologicaDto>>(respuestasPsicologicas);

        return new Response<IList<RespuestaPsicologicaDto>>(respuestasPsicologicasDto);
    }
}
