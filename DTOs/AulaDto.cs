using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AulaDto
    {
        public int Id { get; set; }
        public int Seccion { get; set; }
        public int GradoId { get; set; }
        public int TutorId { get; set; }
        public int EscuelaId { get; set; }
    }
}
