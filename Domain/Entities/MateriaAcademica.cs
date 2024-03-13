using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MateriaAcademica: Entity
    {
        public string Nombre { get; private set; }

        // Relaciones con otras tablas
        public virtual IList<CompetenciaAcademica>? CompetenciasAcademicas { get; private set; }

        // Funciones
        public MateriaAcademica(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void Update(string nombre)
        {
            UpdateNombre(nombre);
        }
    }
}
