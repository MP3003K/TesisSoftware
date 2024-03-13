using Domain.Exceptions.Base;

namespace API.Exceptions;

public class SentRouteIdNotMatchRequestEntityIdException : BaseException
{
    private const string MessageKey = "The Sent Id By Route Didn't Match With Id From Request Body";

    public SentRouteIdNotMatchRequestEntityIdException(int id) : base(MessageKey)
    {
    }
}