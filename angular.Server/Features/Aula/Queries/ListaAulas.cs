using Application.Wrappers;
using AutoMapper;
using DTOs;
using MediatR;
using webapi.Dao.Repositories;

namespace Application.Features.Unidad.Queries
{
    public class ListaAulasQuery: IRequest<Response<IList<AulaDto>>>
    {
    }
    public class ListaAulasHandler: IRequestHandler<ListaAulasQuery, Response<IList<AulaDto>>>
    {
        private readonly IAulaRepository _aulaRepository;
        private readonly IMapper _mapper;

        public ListaAulasHandler(
            IAulaRepository aulaRepository,
            IMapper mapper)
        {
            _aulaRepository = aulaRepository;
            _aulaRepository = aulaRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<AulaDto>>> Handle(ListaAulasQuery request, CancellationToken cancellationToken)
        {
            var aulas = await _aulaRepository.ObtenerTodasLasAulas();
            var aulasDto = _mapper.Map<IList<AulaDto>>(aulas);
            return new Response<IList<AulaDto>>(aulasDto);
        }
    }
}
