using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class ValueNeedsBeGreaterThanZeroException : BaseException
{
    private const string MessageKey = "The Sent Value Needs Be Greater Than Zero";

    public ValueNeedsBeGreaterThanZeroException() : base(MessageKey)
    {
    }
}