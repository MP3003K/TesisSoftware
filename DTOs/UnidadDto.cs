using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UnidadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int NUnidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EscuelaId { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
