using Domain.Entities.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EvaluacionEstudiante: Entity
    {
        public int EvaluacionAulaId { get; private set; }
        public int EstudianteId { get; private set; }

        // Relaciones con otras tablas
        public virtual IList<Indicador>? Indicadores { get; private set; }
        public virtual Estudiante? Estudiante { get; private set; }
        public virtual EvaluacionAula? EvaluacionAula { get; private set; }

        // Funciones
        public EvaluacionEstudiante(int evaluacionAulaId, int estudianteId)
        {
            EvaluacionAulaId = evaluacionAulaId;
            EstudianteId = estudianteId;
        }
        public void UpdateEvaluacionAulaId(int evaluacionAulaId)
        {
            EvaluacionAulaId = evaluacionAulaId;
        }
        public void UpdateEstudianteId(int estudianteId)
        {
            EstudianteId = estudianteId;
        }
        public void Update(int evaluacionAulaId, int estudianteId)
        {
            UpdateEvaluacionAulaId(evaluacionAulaId);
            UpdateEstudianteId(estudianteId);
        }
    }
}
