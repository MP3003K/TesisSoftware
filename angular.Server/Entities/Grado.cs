using Entities.Base;

namespace Entities
{
    public class Grado : Entity
    {
        public string Nombre { get; private set; }
        public int NGrado { get; private set; }
        public int NivelId { get; private set; }
        public virtual Nivel? Nivel { get; private set; }
        public virtual IList<Aula>? Aulas { get; private set; }
        public IList<GradoEvaPsicologica>? GradosEvaPsicologicas { get; private set; }

        public Grado(string nombre, int nGrado, int nivelId)
        {
            Nombre = nombre;
            NGrado = nGrado;
            NivelId = nivelId;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateNGrado(int nGrado)
        {
            NGrado = nGrado;
        }
        public void UpdateNivelId(int nivelId)
        {
            NivelId = nivelId;
        }
        public void Update(string nombre, int nGrado, int nivelId)
        {
            UpdateNombre(nombre);
            UpdateNGrado(nGrado);
            UpdateNivelId(nivelId);
        }
    }
}
