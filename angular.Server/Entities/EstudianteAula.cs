using Entities.Base;

namespace Entities
{
    public class EstudianteAula: Entity
    {
        public int EstudianteId { get; private set; }
        public int AulaId { get; private set; }
        public string Estado { get; private set; }
        public int Anio { get; private set; }
        public virtual Estudiante? Estudiante { get; private set; }
        public virtual Aula? Aula { get; private set; }

        public EstudianteAula(int estudianteId, int aulaId)
        {
            EstudianteId = estudianteId;
            AulaId = aulaId;
            Estado = "1";
            Anio = DateTime.Now.Year;
        }
        public void UpdateEstudianteId(int estudianteId)
        {
            EstudianteId = estudianteId;
        }
        public void UpdateAulaId(int aulaId)
        {
            AulaId = aulaId;
        }
        public void UpdateEstado(string estado)
        {
            Estado = estado;
        }
        public void UpdateAnio(int anio)
        {
            Anio = anio;
        }
        public void Update(int estudianteId, int aulaId, string estado, int anio)
        {
            UpdateEstudianteId(estudianteId);
            UpdateAulaId(aulaId);
            UpdateEstado(estado);
            UpdateAnio(anio);
        }

    }
}
