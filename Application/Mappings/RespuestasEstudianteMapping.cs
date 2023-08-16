using AutoMapper;
using Domain.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class RespuestasEstudianteMapping: Profile
    {
        public RespuestasEstudianteMapping()
        {
            CreateMap<IList<EscalaPsicologicaDto>, RespuestasEstudianteDto>()
                .ReverseMap();
        }
    }
}
