using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EvaluacionAula: Entity
    {
        public int PruebaGradoId { get; private set; }
        public DateTime Fecha { get; private set; }
        public int UnidadId { get; private set; }
        public int AulaId { get; private set; }


        // Relaciones con otras tablas
        public virtual PruebaGrado? PruebaGrado { get; private set; }
        public virtual Unidad? Unidad { get; private set; }
        public virtual Aula? Aula { get; private set; }
        public virtual IList<EvaluacionEstudiante>? EvaluacionesEstudiante { get; private set; }
        
        // Funciones
        public EvaluacionAula(int pruebaGradoId, DateTime fecha, int unidadId, int aulaId)
        {
            PruebaGradoId = pruebaGradoId;
            Fecha = fecha;
            UnidadId = unidadId;
            AulaId = aulaId;
        }
        public void UpdatePruebaGradoId(int pruebaGradoId)
        {
            PruebaGradoId = pruebaGradoId;
        }
        public void UpdateFecha(DateTime fecha)
        {
            Fecha = fecha;
        }
        public void UpdateUnidadId(int unidadId)
        {
            UnidadId = unidadId;
        }
        public void UpdateAulaId(int aulaId)
        {
            AulaId = aulaId;
        }
        public void Update(int pruebaGradoId, DateTime fecha, int unidadId, int aulaId)
        {
            UpdatePruebaGradoId(pruebaGradoId);
            UpdateFecha(fecha);
            UpdateUnidadId(unidadId);
            UpdateAulaId(aulaId);
        }
    }
}
