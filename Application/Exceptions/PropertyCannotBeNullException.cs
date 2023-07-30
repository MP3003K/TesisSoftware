using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class PropertyCannotBeNullException : BaseException
{
    private const string Property = "Property";
    private const string MessageKey = $"The Property {{{Property}}} Cannot Be Null";

    public PropertyCannotBeNullException(string property) : base(MessageKey)
    {
        AddOrReplaceValue(Property, property);
    }
}