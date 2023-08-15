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

namespace Application.Features.Usuario.Queries
{

    public class ValidarUsuarioQuery : IRequest<Response<UnidadDto>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class ValidarUsuarioHandler : IRequestHandler<ValidarUsuarioQuery, Response<UnidadDto>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ValidarUsuarioHandler(
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<Response<UnidadDto>> Handle(ValidarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var userName = request.UserName;
            var password = request.Password;

            var unidad = await _usuarioRepository.ObtenerUsuario(userName, password);
            var unidadDto = _mapper.Map<UnidadDto>(unidad);
            return new Response<UnidadDto>(unidadDto);
        }
    }
}
