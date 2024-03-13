using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class GradoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int NivelId { get; set; }
        public int NGrado { get; set; }
    }
}
