using Application.Wrappers;
using AutoMapper;
using DTOs;
using MediatR;
using webapi.Dao.Repositories;


namespace Application.Features.Unidad.Queries
{
    public class ListaUnidadesQuery: IRequest<Response<IList<UnidadDto>>>
    {
    }
    public class ListaUnidadesHandler: IRequestHandler<ListaUnidadesQuery, Response<IList<UnidadDto>>>
    {
        private readonly IUnidadRepository _unidadRepository;
        private readonly IMapper _mapper;

        public ListaUnidadesHandler(
            IUnidadRepository unidadRepository,
            IMapper mapper)
        {
            _unidadRepository = unidadRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<UnidadDto>>> Handle(ListaUnidadesQuery request, CancellationToken cancellationToken)
        {
            var unidades = await _unidadRepository.ListAllAsync();
            var unidadesDto = _mapper.Map<IList<UnidadDto>>(unidades);
            return new Response<IList<UnidadDto>>(unidadesDto);
        }
    }
}
