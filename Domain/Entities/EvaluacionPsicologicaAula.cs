using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EvaluacionPsicologicaAula : Entity
    {
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public string Estado { get; private set; }
        public int UnidadId { get; private set; }
        public int AulaId { get; private set; }
        public int EvaluacionPsicologicaId { get; private set; }

        // Relaciones con otras tablas
        public virtual Unidad? Unidad { get; private set; }
        public virtual Aula? Aula { get; private set; }
        public virtual IList<EvaluacionPsicologicaEstudiante>? EvaluacionesPsicologicasEstudiante { get; private set; }
        public virtual EvaluacionPsicologica? EvaluacionPsicologica { get; private set; }

        // Funciones
        public EvaluacionPsicologicaAula(int unidadId, int aulaId, int evaluacionPsicologicaId)
        {
            Estado = "N";
            UnidadId = unidadId;
            AulaId = aulaId;
            EvaluacionPsicologicaId = evaluacionPsicologicaId;
        }
        public void UpdateFechaInicio(DateTime fechaInicio)
        {
            FechaInicio = fechaInicio;
        }
        public void UpdateFechaFin(DateTime fechaFin)
        {
            FechaFin = fechaFin;
        }
        public void IniciarEvaluacion(DateTime fechaInicio)
        {
            FechaInicio = fechaInicio;
            Estado = "P";
        }
        public void TerminarEvaluacion(DateTime fechaFin)
        {
            FechaFin= fechaFin;
            Estado = "F";
        }
        public void UpdateEstado(string estado)
        {
            Estado = estado;
        }
        public void UpdateUnidadId(int unidadId)
        {
            UnidadId = unidadId;
        }
        public void UpdateAulaId(int aulaId)
        {
            AulaId = aulaId;
        }
        public void UpdateEvaluacionPsicologicaId(int evaluacionPsicologicaId)
        {
            EvaluacionPsicologicaId = evaluacionPsicologicaId;
        }
        public void Update(DateTime fechaInicio, DateTime fechaFin, string estado, int unidadId, int aulaId, int evaluacionPsicologicaId)
        {
            UpdateFechaInicio(fechaInicio);
            UpdateFechaFin(fechaFin);
            UpdateEstado(estado);
            UpdateUnidadId(unidadId);
            UpdateAulaId(aulaId);
            UpdateEvaluacionPsicologicaId(evaluacionPsicologicaId);
        }

    }
}
