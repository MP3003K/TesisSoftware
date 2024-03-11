using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EntidadNoPuedeSerEliminadaPorSusDependenciasException: BaseException
    {
        private const string Entidad = "Entidad";
        private const string MessageKey = $"La Entidad {{{Entidad}}} No Puede Ser Eliminada Por Que Tiene Algunas Dependencias";

        public EntidadNoPuedeSerEliminadaPorSusDependenciasException(string entidad): base(MessageKey)
        {
            AddOrReplaceValue(Entidad, entidad);
        }
    }
}
