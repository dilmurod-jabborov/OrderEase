namespace OrderEase.Service.Exceptions;

public class AlreadyExistException : Exception
{
    private int statusCode;
    public AlreadyExistException(string message) : base(message)
    {
        statusCode = 403;
    }
}
