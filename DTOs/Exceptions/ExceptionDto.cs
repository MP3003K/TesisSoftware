using Domain.Exceptions.Base;

namespace DTOs.Exceptions;

public class ExceptionDto
{
    public string Key { get; set; }


    public ExceptionDto(BaseException exception)
    {
        Key = exception.Key;
    }

    public ExceptionDto(string key)
    {
        Key = key;
    }
}