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
    public class ListaUnidadesQuery: IRequest<Response<IList<UsuarioDto>>>
    {
    }
    public class ListaUnidadesHandler: IRequestHandler<ListaUnidadesQuery, Response<IList<UsuarioDto>>>
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
        public async Task<Response<IList<UsuarioDto>>> Handle(ListaUnidadesQuery request, CancellationToken cancellationToken)
        {
            var unidades = await _unidadRepository.ListAllAsync();
            var unidadesDto = _mapper.Map<IList<UsuarioDto>>(unidades);
            return new Response<IList<UsuarioDto>>(unidadesDto);
        }
    }
}
