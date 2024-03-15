using AutoMapper;
using Entities;
using DTOs;

namespace angular.Server.Interfaces.Mappings
{
    public class RolAccesoMapping : Profile
    {
        public RolAccesoMapping()
        {
            CreateMap<RolAcceso, RolAccesoDto>().ReverseMap();
        }
    }
}
