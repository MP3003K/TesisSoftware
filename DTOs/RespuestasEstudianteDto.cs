using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RespuestasEstudianteDto
    {
        public IList<EscalaPsicologicaDto> EscalasPsicologicas { get; set; } = new List<EscalaPsicologicaDto>();
        public int? TotalEscalasEnInicio { get; set; }
        public int? TotalEscalasEnProceso { get; set; }
        public int? TotalEscalasSatisfactorio { get; set; }

        public int? TotalIndicadoresEnInicio { get; set; }
        public int? TotalIndicadoresEnProceso { get; set; }
        public int? TotalIndicadoresSatisfactorio { get; set; }
    }
}
