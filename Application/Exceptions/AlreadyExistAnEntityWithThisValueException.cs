using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class AlreadyExistAnEntityWithThisValueException : BaseException
{
    private const string Entity = "Entity";
    private const string Value = "Value";
    private const string MessageKey = $"Already exist an entity {{{Entity}}} with this value:{{{Value}}}";

    public AlreadyExistAnEntityWithThisValueException(string entity, string value) : base(MessageKey)
    {
        AddOrReplaceValue(Entity, entity);
        AddOrReplaceValue(Value, value);
    }
}