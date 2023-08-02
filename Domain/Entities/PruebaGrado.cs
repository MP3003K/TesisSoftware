using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PruebaGrado: Entity
    {
        public int GradoId { get; private set; }
        public int PruebaId { get; private set; }

        
        // Relaciones con otras tablas
        public virtual Grado? Grado { get; private set; }
        public virtual PruebaPsicologica? PruebaPsicologica { get; private set; }
        public virtual IList<EvaluacionAula>? EvaluacionesAula { get; private set; }

        // Funciones
        public PruebaGrado(int gradoId, int pruebaId)
        {
            GradoId = gradoId;
            PruebaId = pruebaId;
        }
        public void UpdateGradoId(int gradoId)
        {
            GradoId = gradoId;
        }
        public void UpdatePruebaId(int pruebaId)
        {
            PruebaId = pruebaId;
        }
        public void Update(int gradoId, int pruebaId)
        {
            UpdateGradoId(gradoId);
            UpdatePruebaId(pruebaId);
        }
    }
}
