﻿using Entities.Base;

namespace Entities
{
    public class Aula: Entity
    {
        public string Secccion { get; private set; }
        public int GradoId { get; private set; }
        public int EscuelaId { get; private set; }
        public virtual Grado? Grado { get; private set; }
        public virtual Escuela? Escuela { get; private set; }
        public virtual IList<EstudianteAula>? EstudiantesAulas { get; private set; }
        public virtual IList<EvaluacionPsicologicaAula>? EvaluacionesPsicologicasAula { get; private set; }
        public virtual IList<EvaluacionCompetenciaEstudiante>? EvaluacionesCompetenciasEstudiante { get; private set; }
        public Aula(string secccion, int gradoId, int escuelaId)
        {
            Secccion = secccion;
            GradoId = gradoId;
            EscuelaId = escuelaId;
        }
        public void UpdateSecccion(string secccion)
        {
            Secccion = secccion;
        }
        public void UpdateGradoId(int gradoId)
        {
            GradoId = gradoId;
        }
        public void UpdateEscuelaId(int escuelaId)
        {
            EscuelaId = escuelaId;
        }
        public void Update(string secccion, int gradoId, int escuelaId)
        {
            UpdateSecccion(secccion);
            UpdateGradoId(gradoId);
            UpdateEscuelaId(escuelaId);
        }

    }
}
