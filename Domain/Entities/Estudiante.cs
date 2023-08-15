using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Estudiante: Entity
    {
        public string CodigoEstudiante { get; private set; }
        public int PersonaId { get; private set; }


        // Relaciones con otras tablas
        public virtual IList<EstudianteAula>? EstudiantesAulas{ get; private set; }
        public virtual Persona? Persona { get; private set; }
        public virtual IList<EvaluacionPsicologicaEstudiante>? EvaluacionesEstudiante { get; private set; }


        // Funciones
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
