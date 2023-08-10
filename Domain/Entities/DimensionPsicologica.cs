using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DimensionPsicologica: Entity
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public virtual IList<EscalaPsicologica>? EscalasPsicologicas { get; private set; }
        public DimensionPsicologica(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
        public void UpdateCodigo(string codigo)
        {
            Codigo = codigo;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void Update(string codigo, string nombre)
        {
            UpdateCodigo(codigo);
            UpdateNombre(nombre);
        }
        
    }
}
