using Domain.Exceptions.Base;

namespace Application.Exceptions
{
    public class MensajePersonalizadoException : BaseException
    {
        private const string Mensaje = "Mensaje";
        private const string MessageKey = $"{{{Mensaje}}}";

        public MensajePersonalizadoException(string mensaje) : base(MessageKey)
        {
            AddOrReplaceValue(Mensaje, mensaje);
        }
    }
}
