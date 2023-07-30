using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class EntityCannotBeRemovedByDependencyException : BaseException
{
    private const string Entity = "Entity";
    private const string MessageKey = $"The Entity {{{Entity}}} Cannot Be Remove Because There Are Some Dependencies";

    public EntityCannotBeRemovedByDependencyException(string entity) : base(MessageKey)
    {
        AddOrReplaceValue(Entity, entity);
    }
}