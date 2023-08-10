using Domain.Entities.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EvaluacionPsicologicaEstudiante : Entity
    {
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public string Estado { get; private set; }
        public int EvaluacionAulaId { get; private set; }
        public int EstudianteId { get; private set; }
        public int RespuestaPsicologicaId { get; private set; }

        // Relaciones con otras tablas
        public virtual Estudiante? Estudiante { get; private set; }
        public virtual EvaluacionPsicologicaAula? EvaluacionAula { get; private set; }
        public virtual IList<RespuestaPsicologica>? RespuestasPsicologicas { get; private set; }

        // Funciones
        public EvaluacionPsicologicaEstudiante(DateTime fechaInicio, DateTime fechaFin, string estado, int evaluacionAulaId, int estudianteId, int respuestaPsicologicaId)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Estado = estado;
            EvaluacionAulaId = evaluacionAulaId;
            EstudianteId = estudianteId;
            RespuestaPsicologicaId = respuestaPsicologicaId;
        }
        public void UpdateFechaInicio(DateTime fechaInicio)
        {
            FechaInicio = fechaInicio;
        }
        public void UpdateFechaFin(DateTime fechaFin)
        {
            FechaFin = fechaFin;
        }
        public void UpdateEstado(string estado)
        {
            Estado = estado;
        }
        public void UpdateEvaluacionAulaId(int evaluacionAulaId)
        {
            EvaluacionAulaId = evaluacionAulaId;
        }
        public void UpdateEstudianteId(int estudianteId)
        {
            EstudianteId = estudianteId;
        }
        public void UpdateRespuestaPsicologicaId(int respuestaPsicologicaId)
        {
            RespuestaPsicologicaId = respuestaPsicologicaId;
        }
        public void Update(DateTime fechaInicio, DateTime fechaFin, string estado, int evaluacionAulaId, int estudianteId, int respuestaPsicologicaId)
        {
            UpdateFechaInicio(fechaInicio);
            UpdateFechaFin(fechaFin);
            UpdateEstado(estado);
            UpdateEvaluacionAulaId(evaluacionAulaId);
            UpdateEstudianteId(estudianteId);
            UpdateRespuestaPsicologicaId(respuestaPsicologicaId);
        }

    }
}
