using AutoMapper;
using Entities;
using DTOs;


namespace Interfaces.Mappings
{
    public class UsuarioMapping : Profile
    {
        public UsuarioMapping()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
