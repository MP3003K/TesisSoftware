using Entities.Base;

namespace Entities
{
    public class GradoEvaPsicologica: Entity
    {
        public int GradoId { get; private set; }
        public int EvaPsiId { get; private set; }
        public virtual Grado? Grado { get; private set; }
        public virtual EvaluacionPsicologica? EvaluacionPsicologica{ get; private set; }

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
