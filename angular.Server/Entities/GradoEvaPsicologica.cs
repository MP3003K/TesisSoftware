using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GradoEvaPsicologica: Entity
    {
        public int GradoId { get; private set; }
        public int EvaPsiId { get; private set; }
        
        // Relaciones con otras tablas
        public virtual Grado? Grado { get; private set; }
        public virtual EvaluacionPsicologica? EvaluacionPsicologica{ get; private set; }

        // Funciones
        public GradoEvaPsicologica(int gradoId, int evaPsiId)
        {
            GradoId = gradoId;
            EvaPsiId = evaPsiId;
        }
        public void UpdateGradoId(int gradoId)
        {
            GradoId = gradoId;
        }
        public void UpdateEvaPsiId(int evaPsiId)
        {
            EvaPsiId = evaPsiId;
        }
        public void Update(int gradoId, int evaPsiId)
        {
            UpdateGradoId(gradoId);
            UpdateEvaPsiId(evaPsiId);
        }
    }
}
