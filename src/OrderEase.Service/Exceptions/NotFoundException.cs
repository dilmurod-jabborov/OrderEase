using OrderEase.Domain.Enums;

namespace OrderEase.Service.Exceptions;

public class NotFoundException : Exception
{
    private int statusCode;
    public NotFoundException(string message) : base(message)
    {
        statusCode = 404;
    }
}
