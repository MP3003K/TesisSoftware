using Entities.Base;

namespace Entities
{
    public class CompetenciaAcademica: Entity
    {
        public string Nombre { get; private set; }
        public int MateriaAcademicaId { get; private set; }
        public virtual MateriaAcademica? MateriaAcademica { get; private set; }
        public virtual IList<EvaluacionCompetenciaEstudiante>? EvaluacionesCompetenciasEstudiante { get; private set; }

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
