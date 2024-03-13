using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
