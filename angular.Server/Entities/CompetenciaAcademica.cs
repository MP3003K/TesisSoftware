using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CompetenciaAcademica: Entity
    {
        public string Nombre { get; private set; }
        public int MateriaAcademicaId { get; private set; }

        // Relaciones con otras tablas
        public virtual MateriaAcademica? MateriaAcademica { get; private set; }
        public virtual IList<EvaluacionCompetenciaEstudiante>? EvaluacionesCompetenciasEstudiante { get; private set; }

        // Funciones
        public CompetenciaAcademica(string nombre, int materiaAcademicaId)
        {
            Nombre = nombre;
            MateriaAcademicaId = materiaAcademicaId;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void Update(int materiaAcademicaId)
        {
            MateriaAcademicaId = materiaAcademicaId;
        }
    }
}
