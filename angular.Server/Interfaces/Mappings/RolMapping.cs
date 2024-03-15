using AutoMapper;
using Entities;
using DTOs;

namespace angular.Server.Interfaces.Mappings
{
    public class RolMapping : Profile
    {
        public RolMapping()
        {
            CreateMap<Rol, RolDto>().ReverseMap();
        }
    }
}
