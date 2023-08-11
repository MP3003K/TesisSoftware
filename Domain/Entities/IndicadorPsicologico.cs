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
        public int EscalaPsicologicaId { get; private set; }

        // Relaciones con otras tablas
        public virtual EscalaPsicologica? Escala { get; private set; }
        public virtual  IList<PreguntaPsicologica>? PreguntasPsicologicas { get; private set; }
        public virtual  EscalaPsicologica? EscalaPsicologica { get; private set; }

        //Funciones
        public IndicadorPsicologico(string nombreIndicador, int escalaPsicologicaId)
        {
            NombreIndicador = nombreIndicador;
            EscalaPsicologicaId = escalaPsicologicaId;
        }
        public void UpdateNombreIndicador(string nombreIndicador)
        {
            NombreIndicador = nombreIndicador;
        }
        public void UpdateEscalaPsicologicaId(int escalaPsicologicaId)
        {
            EscalaPsicologicaId = escalaPsicologicaId;
        }
        public void Update(string nombreIndicador, int escalaPsicologicaId)
        {
            UpdateNombreIndicador(nombreIndicador);
            UpdateEscalaPsicologicaId(escalaPsicologicaId);
        }



    }
}
