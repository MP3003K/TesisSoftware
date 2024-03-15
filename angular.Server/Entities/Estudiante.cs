using Entities.Base;

namespace Entities
{
    public class Estudiante: Entity
    {
        public string CodigoEstudiante { get; private set; }
        public int PersonaId { get; private set; }
        public virtual IList<EstudianteAula>? EstudiantesAulas{ get; private set; }
        public virtual Persona? Persona { get; private set; }
        public virtual IList<EvaluacionPsicologicaEstudiante>? EvaluacionesEstudiante { get; private set; }
        public virtual IList<EvaluacionCompetenciaEstudiante>? EvaluacionesCompetenciasEstudiante { get; private set; }

        public Estudiante(string codigoEstudiante, int personaId)
        {
            CodigoEstudiante = codigoEstudiante;
            PersonaId = personaId;
        }
        public void UpdateCodigoEstudiante(string codigoEstudiante)
        {
            CodigoEstudiante = codigoEstudiante;
        }
        public void UpdatePersonaId(int personaId)
        {
            PersonaId = personaId;
        }
        public void Update(string codigoEstudiante, int personaId)
        {
            UpdateCodigoEstudiante(codigoEstudiante);
            UpdatePersonaId(personaId);
        }

    }
}
