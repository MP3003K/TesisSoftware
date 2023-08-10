using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RespuestaPsicologicaDto
    {
        public int Id { get; set; }
        public string Respuesta { get; set; } = string.Empty;
        public int Puntaje { get; set; }
        public int PreguntaId { get; set; }
        public int EvaPsiEstId { get; set; }
    }
}
