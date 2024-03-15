using AutoMapper;
using Entities;
using DTOs;

namespace Interfaces.Mappings
{
    public class AccesoMapping : Profile
    {
        public AccesoMapping()
        {
            CreateMap<Acceso, RolAccesoDto>().ReverseMap();
        }
    }
}
