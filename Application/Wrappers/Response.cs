using DTOs.Exceptions;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string? message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(ExceptionDto exception)
        {
            Succeeded = false;
            Message = exception.Key;
        }

        public Response(string? message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}