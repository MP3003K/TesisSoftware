using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Escala : Entity
    {
        public string Nombre { get; private set; }
        public int DimensionId { get; private set; }
        public virtual Dimension? Dimension { get; private set; }
        public IList<Indicador>? Indicadores { get; private set; }
        public Escala(string nombre, int dimensionId)
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
