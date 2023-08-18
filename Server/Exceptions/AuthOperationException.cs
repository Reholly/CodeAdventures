namespace Server.Exceptions;

public class AuthOperationException : InvalidOperationException
{
    public override string Message { get; }

    public AuthOperationException(string message)
        :base(message)
    {
        Message = message;
    }
}