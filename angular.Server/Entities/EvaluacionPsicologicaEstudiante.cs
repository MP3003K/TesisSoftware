using Entities.Base;

namespace Entities
{
    public class EvaluacionPsicologicaEstudiante : Entity
    {
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public string Estado { get; private set; }
        public int EvaluacionAulaId { get; private set; }
        public int EstudianteId { get; private set; }
        public virtual Estudiante? Estudiante { get; private set; }
        public virtual EvaluacionPsicologicaAula? EvaluacionAula { get; private set; }
        public virtual IList<RespuestaPsicologica>? RespuestasPsicologicas { get; private set; }

        public EvaluacionPsicologicaEstudiante(int evaluacionAulaId, int estudianteId)
        {
            Estado = "N";
            EvaluacionAulaId = evaluacionAulaId;
            EstudianteId = estudianteId;
        }
        public void UpdateFechaInicio(DateTime fechaInicio)
        {
            FechaInicio = fechaInicio;
        }

        public void IniciarEvaluacion(DateTime fechaInicio)
        {
            FechaInicio = fechaInicio;
            Estado = "P";
        }
        public void TerminarEvaluacion(DateTime fechaFin)
        {
            FechaFin = fechaFin;
            Estado = "F";
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
        public void Update(DateTime fechaInicio, DateTime fechaFin, string estado, int evaluacionAulaId, int estudianteId)
        {
            UpdateFechaInicio(fechaInicio);
            UpdateFechaFin(fechaFin);
            UpdateEstado(estado);
            UpdateEvaluacionAulaId(evaluacionAulaId);
            UpdateEstudianteId(estudianteId);
        }


    }
}
