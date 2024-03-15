using Entities.Base;

namespace Entities
{
    public class EscalaPsicologica : Entity
    {
        public string Nombre { get; private set; }
        public int DimensionId { get; private set; }
        public virtual DimensionPsicologica? DimensionPsicologica { get; private set; }
        public IList<IndicadorPsicologico>? IndicadoresPsicologicos { get; private set; }
        
        public EscalaPsicologica(string nombre, int dimensionId)
        {
            Nombre = nombre;
            DimensionId = dimensionId;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateDimensionId(int dimensionId)
        {
            DimensionId = dimensionId;
        }
        public void Update(string nombre, int dimensionId)
        {
            UpdateNombre(nombre);
            UpdateDimensionId(dimensionId);
        }
    }
}
