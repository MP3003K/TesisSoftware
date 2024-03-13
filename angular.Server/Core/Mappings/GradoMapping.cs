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
    public class GradoMapping: Profile
    {
        public GradoMapping()
        {
            CreateMap<Grado, GradoDto>()
                .ReverseMap();
        }
    }
}
