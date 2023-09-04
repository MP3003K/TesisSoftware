using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RespuestasEstudianteDto
    {
        public double PromedioEvaluacionPsicologica { get; set; } = 0;
        public IList<EscalaPsicologicaDto> EscalasPsicologicas { get; set; } = new List<EscalaPsicologicaDto>();
        public int TotalEscalasEnInicio { get; set; } = 0;
        public int TotalEscalasEnProceso { get; set; } = 0;
        public int TotalEscalasSatisfactorio { get; set; } = 0;

        public int TotalIndicadoresEnInicio { get; set; } = 0;
        public int TotalIndicadoresEnProceso { get; set; } = 0;
        public int TotalIndicadoresSatisfactorio { get; set; } = 0;
    }
}
