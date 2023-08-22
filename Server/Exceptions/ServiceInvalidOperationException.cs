namespace Server.Exceptions;

public class ServiceInvalidOperationException : InvalidOperationException
{
    public override string Message { get; }

    public ServiceInvalidOperationException(string message)
        :base(message)
    {
        Message = message;
    }
}