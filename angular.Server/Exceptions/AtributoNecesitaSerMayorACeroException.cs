
using Domain.Exceptions.Base;

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
