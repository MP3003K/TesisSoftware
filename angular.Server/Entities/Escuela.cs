using Entities.Base;

namespace Entities
{
    public class Escuela: Entity
    {
        public string CodigoModular { get; private set; }
        public string Nombre { get; private set; }
        public virtual IList<Unidad>? Unidades{ get; private set; }
        public virtual IList<Aula>? Aulas { get; private set; }

        public Escuela(string codigoModular, string nombre)
        {
            CodigoModular = codigoModular;
            Nombre = nombre;
        }
        public void UpdateCodigoModular(string codigoModular)
        {
            CodigoModular = codigoModular;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void Update(string codigoModular, string nombre)
        {
            UpdateCodigoModular(codigoModular);
            UpdateNombre(nombre);
        }
    }
}
