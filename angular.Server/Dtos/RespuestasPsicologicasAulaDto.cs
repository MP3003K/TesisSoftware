using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RespuestasPsicologicasAulaDto
    {
        public IList<RespuestasPsicologicasEstudianteDto>? RespuestasEstudiantesDto { get; set; }
        public IList<EscalaPsicologicaDto>? RespuestaAulaDto { get; set; }
    }
}
