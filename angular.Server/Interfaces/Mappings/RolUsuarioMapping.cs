using AutoMapper;
using Entities;
using DTOs;

namespace angular.Server.Interfaces.Mappings
{
    public class RolUsuarioMapping : Profile
    {
        public RolUsuarioMapping()
        {
            CreateMap<RolUsuario, RolUsuarioDto>().ReverseMap();
        }
    }
}
