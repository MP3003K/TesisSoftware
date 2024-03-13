using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Nivel: Entity
    {
        public string Nombre { get; private set; }
        public int NNivel { get; private set; }

        // Relaciones con otras tablas
        public virtual IList<Grado>? Grados { get; private set; }

        //Funciones
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
