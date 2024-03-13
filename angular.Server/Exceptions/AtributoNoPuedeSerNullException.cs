using Domain.Exceptions.Base;


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
