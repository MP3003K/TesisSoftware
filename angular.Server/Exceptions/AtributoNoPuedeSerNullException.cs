using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AtributoNoPuedeSerNullException: BaseException
    {
        public const string Atributo = "Atributo";
        public const string MessageKey = $"El Atributo {{{Atributo}}} No Puede Ser Null ";
        public AtributoNoPuedeSerNullException(string atributo) : base(MessageKey)
        {
            AddOrReplaceValue(Atributo, atributo);
        }
    }
}
