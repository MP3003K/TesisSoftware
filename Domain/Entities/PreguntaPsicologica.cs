using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PreguntaPsicologica: Entity
    {
        public string Pregunta { get; private set; }
        public int IndicadorPsicologicoId { get; private set; }

        // Relaciones con otras tablas
        public virtual IndicadorPsicologico? IndicadorPsicologico { get; private set; }
        public virtual IList<RespuestaPsicologica>? RespuestasPsicologicas { get; private set; }


        // Funciones
        public PreguntaPsicologica(string pregunta, int indicadorPsicologicoId)
        {
            Pregunta = pregunta;
            IndicadorPsicologicoId = indicadorPsicologicoId;
        }
        public void UpdatePregunta(string pregunta)
        {
            Pregunta = pregunta;
        }
        public void UpdateIndicadorPsicologicoId(int indicadorPsicologicoId)
        {
            IndicadorPsicologicoId = indicadorPsicologicoId;
        }
        public void Update(string pregunta, int indicadorPsicologicoId)
        {
            UpdatePregunta(pregunta);
            UpdateIndicadorPsicologicoId(indicadorPsicologicoId);
        }
    }
}
