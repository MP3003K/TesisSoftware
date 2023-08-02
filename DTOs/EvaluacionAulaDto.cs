using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EvaluacionAulaDto
    {
        public int Id { get; set; }
        public int PruebaGradoId { get; set; }
        public DateTime Fecha { get; set; }
        public int UnidadId { get; set; }
        public int AulaId { get; set; }
    }
}
