using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PreguntaPsicologicaDto
    {
        public int Id { get; set; }
        public string Pregunta { get; set; } = string.Empty;
        public int IndicadorPsicologicoId { get; set; }
        public int NPregunta { get; set; }
        public string? Respuesta { get; set; } = string.Empty;
    }
}
