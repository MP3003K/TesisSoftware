using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EntidadNoEncontradaException: BaseException
    {
        private const string Entity = "Entity";
        private const string MessageKey = $"La Entidad {{Entity}} No Ha Podido Ser Encontrada";
        public EntidadNoEncontradaException(string entity) : base(MessageKey)
        {
            AddOrReplaceValue(Entity, entity);
        }
    }
}
