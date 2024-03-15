using Entities.Base;

namespace Entities
{
    public class EvaluacionPsicologica: Entity
    {
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string Tipo { get; private set; }
        public string Estado { get; private set; }
        public int CantPreguntas { get; private set; } = 0;
        public virtual IList<EvaluacionPsicologicaAula>? EvaluacionesPsicologicasAula { get; private set; }
        public IList<GradoEvaPsicologica>? GradosEvaPsicologicas { get; private set; }
        public IList<DimensionPsicologica>? DimensionesPsicologicas { get; private set; }

        public EvaluacionPsicologica(string nombre, string descripcion, string tipo, string estado, int cantPreguntas)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Tipo = tipo;
            Estado = estado;
            CantPreguntas = cantPreguntas;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }
        public void UpdateTipo(string tipo)
        {
            Tipo = tipo;
        }
        public void UpdateEstado(string estado)
        {
            Estado = estado;
        }
        public void UpdateCantPreguntas(int cantPreguntas)
        {
            this.CantPreguntas = cantPreguntas;
        }
        public void Update(string nombre, string descripcion, string tipo, string estado, int cantPreguntas)
        {
            UpdateNombre(nombre);
            UpdateDescripcion(descripcion);
            UpdateTipo(tipo);
            UpdateEstado(estado);
            UpdateCantPreguntas(cantPreguntas);
        }
    }
}
