using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
