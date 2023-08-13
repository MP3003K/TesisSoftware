using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Unidad.Queries
{
    public class UnidadActualQuery: IRequest<Response<UnidadDto>>
    {
    }
    public class UnidadActualHandler: IRequestHandler<UnidadActualQuery, Response<UnidadDto>>
    {
        private readonly IUnidadRepository _unidadRepository;
        private readonly IMapper _mapper;

        public UnidadActualHandler(
                                             IUnidadRepository unidadRepository,
                                                                                                               IMapper mapper)
        {
            _unidadRepository = unidadRepository;
            _mapper = mapper;
        }
        public async Task<Response<UnidadDto>> Handle(UnidadActualQuery request, CancellationToken cancellationToken)
        {
            var unidad = await _unidadRepository.UnidadActual();
            var unidadDto = _mapper.Map<UnidadDto>(unidad);
            return new Response<UnidadDto>(unidadDto);
        }
    }
}
