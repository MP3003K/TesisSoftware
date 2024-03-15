using Entities.Base;

namespace Entities
{
    public class RespuestaPsicologica: Entity
    {
        public string Respuesta { get; private set; }
        public int PreguntaPsicologicaId { get; private set; }
        public int EvaPsiEstId { get; private set; }
        public int Puntaje { get; private set; } = 0;
        public virtual PreguntaPsicologica? PreguntaPsicologica { get; private set; }
        public virtual EvaluacionPsicologicaEstudiante? EvaluacionPsicologicaEstudiante { get; private set; }

        public RespuestaPsicologica(string respuesta, int preguntaPsicologicaId, int evaPsiEstId)
        {
            Respuesta = respuesta;
            PreguntaPsicologicaId = preguntaPsicologicaId;
            EvaPsiEstId = evaPsiEstId;
        }
        public void UpdateRespuesta(string respuesta)
        {
            Respuesta = respuesta;
        }
        public void UpdatePreguntaPsicologicaId(int preguntaPsicologicaId)
        {
            PreguntaPsicologicaId = preguntaPsicologicaId;
        }
        public void UpdateEvaPsiEstId(int evaPsiEstId)
        {
            EvaPsiEstId = evaPsiEstId;
        }
        public void Update(string respuesta, int preguntaPsicologicaId, int evaPsiEstId)
        {
            UpdateRespuesta(respuesta);
            UpdatePreguntaPsicologicaId(preguntaPsicologicaId);
            UpdateEvaPsiEstId(evaPsiEstId);
        }

    }
}
