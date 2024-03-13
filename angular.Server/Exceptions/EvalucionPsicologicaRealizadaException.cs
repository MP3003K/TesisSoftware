using Domain.Exceptions.Base;

namespace Application.Exceptions
{
    public class EvalucionPsicologicaRealizadaException : BaseException
    {
        private const string MessageKey = $"El estudiante ya ha realizado el Test Psicologico de la Unidad";

        public EvalucionPsicologicaRealizadaException(): base(MessageKey)
        {
        }
    }
}
