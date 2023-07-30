using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class EntityNotFoundException : BaseException
{
    private const string Entity = "Entity";
    private const string MessageKey = $"The Entity {{{Entity}}} Cannot Found";

    public EntityNotFoundException(string entity) : base(MessageKey)
    {
        AddOrReplaceValue(Entity, entity);
    }
}