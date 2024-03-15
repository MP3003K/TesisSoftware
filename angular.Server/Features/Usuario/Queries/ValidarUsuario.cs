using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using DTOs;
using MediatR;
using webapi.Dao.Repositories;

namespace Application.Features.Usuario.Queries
{

    public class ValidarUsuarioQuery : IRequest<Response<InformacionUsuarioDto>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class ValidarUsuarioHandler : IRequestHandler<ValidarUsuarioQuery, Response<InformacionUsuarioDto>>
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
        public async Task<Response<InformacionUsuarioDto>> Handle(ValidarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(request.UserName, request.Password) ?? throw new EntidadNoEncontradaException(nameof(Domain.Entities.Usuario));

            if (usuario.Persona is null)
                throw new EntidadNoEncontradaException(nameof(Persona));
            if (usuario.RolesUsuarios is null)
                throw new EntidadNoEncontradaException(nameof(RolUsuario));
            if (usuario.RolesUsuarios.FirstOrDefault() is null)
                throw new EntidadNoEncontradaException(nameof(RolUsuario));

            Rol? rol = usuario.RolesUsuarios.FirstOrDefault().Rol;

            var informacionUsuarioDto = new InformacionUsuarioDto()
            {
                Persona = _mapper.Map<PersonaDto>(usuario.Persona),
                Rol = _mapper.Map<RolDto>(rol)
            };
            return new Response<InformacionUsuarioDto>(informacionUsuarioDto);
        }
    }
}
