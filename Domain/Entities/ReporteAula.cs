using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReporteAula: Entity
    {
        public int IdEstudiante { get; private set; }
        public string Nombres { get; private set; }
        public  int Promedio { get; private set; }
        public  int rango_1 { get; private set; }
        public  int rango_2 { get; private set; }
        public  int rango_3 { get; private set; }

    }
}