using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EvaluacionPsicologica: Entity
    {
        public string Nombre { get; private set; }

        public string Descripcion { get; private set; }
        public string Tipo { get; private set; }
        public string Estado { get; private set; }


        // Relaciones con otras tablas
        public virtual IList<EvaluacionPsicologicaAula>? EvaluacionesPsicologicasAula { get; private set; }

        // Funciones
        public EvaluacionPsicologica(string nombre, string descripcion, string tipo, string estado)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Tipo = tipo;
            Estado = estado;
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
        public void Update(string nombre, string descripcion, string tipo, string estado)
        {
            UpdateNombre(nombre);
            UpdateDescripcion(descripcion);
            UpdateTipo(tipo);
            UpdateEstado(estado);
        }
    }
}
