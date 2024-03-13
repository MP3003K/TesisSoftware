using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EstudianteAulaDto
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int AulaId { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
