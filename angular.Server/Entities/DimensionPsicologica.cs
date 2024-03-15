using Entities.Base;

namespace Entities
{
    public class DimensionPsicologica: Entity
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public int EvaluacionPsicologicaId { get; private set; }
        public virtual IList<EscalaPsicologica>? EscalasPsicologicas { get; private set; }
        public virtual EvaluacionPsicologica? EvaluacionPsicologica { get; private set; }

        public DimensionPsicologica(string codigo, string nombre, int evaluacionPsicologicaId)
        {
            Codigo = codigo;
            Nombre = nombre;
            EvaluacionPsicologicaId = evaluacionPsicologicaId;
        }
        public void UpdateCodigo(string codigo)
        {
            Codigo = codigo;
        }

        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateEvaluacionPsicologicaId(int evaluacionPsicologicaId)
        {
            EvaluacionPsicologicaId = evaluacionPsicologicaId;
        }
        public void Update(string codigo, string nombre, int evaluacionPsicologicaId)
        {
            UpdateCodigo(codigo);
            UpdateNombre(nombre);
            UpdateEvaluacionPsicologicaId(evaluacionPsicologicaId);
        }


    }
}
