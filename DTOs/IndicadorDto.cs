using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class IndicadorDto
    {
        public int Id { get; set; }
        public string NombreIndicador { get; set; } = string.Empty;
        public string Pregunta { get; set; } = string.Empty;
        public  int Puntaje { get; set; }
        public int EscalaId { get; set; }
    }
}
