using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EvaluacionCompetenciaEstudiante: Entity
    {
        public int AulaId { get; private set; }
        public int UnidadId { get; private set; }
        public int CompetenciaAcademicaId { get; private set; }
        public int EstudianteId { get; private set; }
        public string Nota { get; private set; }
        public virtual Aula? Aula { get; private set; }
        public virtual Unidad? Unidad { get; private set; }
        public virtual CompetenciaAcademica? CompetenciaAcademica { get; private set; }
        public virtual Estudiante? Estudiante { get; private set; }

        public EvaluacionCompetenciaEstudiante(int aulaId, int unidadId, int competenciaAcademicaId, int estudianteId, string nota)
        {
            AulaId = aulaId;
            UnidadId = unidadId;
            CompetenciaAcademicaId = competenciaAcademicaId;
            EstudianteId = estudianteId;
            Nota = nota;
        }
        public void UpdateAulaId(int aulaId)
        {
            AulaId = aulaId;
        }
        public void UpdateUnidadId(int unidadId)
        {
            UnidadId = unidadId;
        }
        public void UpdateCompetenciaAcademicaId(int competenciaAcademicaId)
        {
            CompetenciaAcademicaId = competenciaAcademicaId;
        }
        public void UpdateEstudianteId(int estudianteId)
        {
            EstudianteId = estudianteId;
        }
        public void UpdateNota(string nota)
        {
            Nota = nota;
        }
        public void Update(int aulaId, int unidadId, int competenciaAcademicaId, int estudianteId, string nota)
        {
            UpdateAulaId(aulaId);
            UpdateUnidadId(unidadId);
            UpdateCompetenciaAcademicaId(competenciaAcademicaId);
            UpdateEstudianteId(estudianteId);
            UpdateNota(nota);
        }
    }
}
