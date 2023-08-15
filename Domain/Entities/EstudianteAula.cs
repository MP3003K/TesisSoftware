using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EstudianteAula: Entity
    {
        public int EstudianteId { get; private set; }
        public int AulaId { get; private set; }
        public string Estado { get; private set; }

        // Relaciones con otras tablas
        public virtual Estudiante? Estudiante { get; private set; }
        public virtual Aula? Aula { get; private set; }

        // Funciones
        public EstudianteAula(int estudianteId, int aulaId, string estado)
        {
            EstudianteId = estudianteId;
            AulaId = aulaId;
            Estado = estado;
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
        public void Update(int estudianteId, int aulaId, string estado)
        {
            UpdateEstudianteId(estudianteId);
            UpdateAulaId(aulaId);
            UpdateEstado(estado);
        }


    }
}
