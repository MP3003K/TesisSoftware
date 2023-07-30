using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class AlreadyExistAKeyWithThisValueException : BaseException
{
    private const string PixKey = "PixKey";
    private const string MessageKey = $"Already exist a key with this value:{{{PixKey}}}";

    public AlreadyExistAKeyWithThisValueException(string value) : base(MessageKey)
    {
        AddOrReplaceValue(PixKey, value);
    }
}