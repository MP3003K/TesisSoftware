using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Aula: Entity
    {
        public int Secccion { get; private set; }
        public int GradoId { get; private set; }

        public int TutorId { get; private set; }
        public int EscuelaId { get; private set; }


        // Relaciones con otras tablas
        public virtual Grado? Grado { get; private set; }
        public virtual Docente? Tutor { get; private set; }
        public virtual Escuela? Escuela { get; private set; }
        public virtual IList<Estudiante>? Estudiantes { get; private set; }
        public virtual IList<EvaluacionAula>? EvaluacionesAula { get; private set; }

        public Aula(int secccion, int gradoId, int tutorId, int escuelaId)
        {
            Secccion = secccion;
            GradoId = gradoId;
            TutorId = tutorId;
            EscuelaId = escuelaId;
        }
        public void UpdateSecccion(int secccion)
        {
            Secccion = secccion;
        }
        public void UpdateGradoId(int gradoId)
        {
            GradoId = gradoId;
        }
        public void UpdateTutorId(int tutorId)
        {
            TutorId = tutorId;
        }
        public void UpdateEscuelaId(int escuelaId)
        {
            EscuelaId = escuelaId;
        }
        public void Update(int secccion, int gradoId, int tutorId, int escuelaId)
        {
            UpdateSecccion(secccion);
            UpdateGradoId(gradoId);
            UpdateTutorId(tutorId);
            UpdateEscuelaId(escuelaId);
        }
    }
}
