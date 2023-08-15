using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ResultadosPsicologicosEstudiantesDto
    {
        public IList<UnidadDto>? EscalaPsicologica { get; set; }
    }
}
