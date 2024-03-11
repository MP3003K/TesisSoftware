using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AtributoNecesitaSerMayorACeroException: BaseException
    {
        private const string Atributo = "Atributo";
        private const string MessageKey = $"El Atributo {{{Atributo}}} Debe Ser Mayor A Cero";
        public AtributoNecesitaSerMayorACeroException(string atributo) : base(MessageKey)
        {
            AddOrReplaceValue(Atributo, atributo);
        }
    }
}
