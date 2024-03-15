using Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ReporteAula: Entity
    {
        [Key]
        public int IdEstudiante { get; private set; }
        public string Nombres { get; private set; }
        public  int Promedio { get; private set; }
        public  int rango_1 { get; private set; }
        public  int rango_2 { get; private set; }
        public  int rango_3 { get; private set; }

    }
}