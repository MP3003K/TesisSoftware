using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class IndicadorPsicologicoDto
    {
        public int Id { get; set; }
        public string NombreIndicador { get; set; } = string.Empty;
        public int EscalaPsicologicaId { get; set; }
        public double? PromedioIndicador { get; set; }
    }
}
