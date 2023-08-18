namespace Server.Exceptions;

public class ServiceOperationNullException : InvalidOperationException  
{
    public override string Message { get; }

    public ServiceOperationNullException(string message)
        :base(message)
    {
        Message = message;
    }
}