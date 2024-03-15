using Entities.Base;

namespace Entities
{
    public class MateriaAcademica: Entity
    {
        public string Nombre { get; private set; }
        public virtual IList<CompetenciaAcademica>? CompetenciasAcademicas { get; private set; }

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
