using Entities.Base;

namespace Entities
{
    public class Nivel: Entity
    {
        public string Nombre { get; private set; }
        public int NNivel { get; private set; }
        public virtual IList<Grado>? Grados { get; private set; }

        public Nivel(string nombre, int nNivel)
        {
            Nombre = nombre;
            NNivel = nNivel;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateNNivel(int nNivel)
        {
            NNivel = nNivel;
        }
        public void Update(string nombre, int nNivel)
        {
            UpdateNombre(nombre);
            UpdateNNivel(nNivel);
        }

    }
}
