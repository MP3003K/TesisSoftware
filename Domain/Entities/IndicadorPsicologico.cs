using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IndicadorPsicologico: Entity
    {
        public string NombreIndicador { get; private set; }
        public int EscalaId { get; private set; }
        public virtual EscalaPsicologica? Escala { get; private set; }

        // Relaciones con otras tablas
        public virtual  IList<PreguntaPsicologica>? PreguntasPsicologicas { get; private set; }
        public virtual  EscalaPsicologica? EscalaPsicologica { get; private set; }

        //Funciones
        public IndicadorPsicologico(string nombreIndicador, int escalaId)
        {
            NombreIndicador = nombreIndicador;
            EscalaId = escalaId;
        }
        public void UpdateNombreIndicador(string nombreIndicador)
        {
            NombreIndicador = nombreIndicador;
        }
        public void UpdateEscalaId(int escalaId)
        {
            EscalaId = escalaId;
        }
        public void Update(string nombreIndicador, int escalaId)
        {
            UpdateNombreIndicador(nombreIndicador);
            UpdateEscalaId(escalaId);
        }


    }
}
