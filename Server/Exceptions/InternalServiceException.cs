namespace Server.Exceptions;

public class InternalServiceException : InvalidOperationException
{
    public override string Message { get; }

    public InternalServiceException(string message = "") : base(message)
    {
        Message = message;
    }
}