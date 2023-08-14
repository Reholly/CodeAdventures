namespace Server.Services.AuthServices.AuthResponses;

public record LogInResponse
{
    public required bool IsSucceeded { get; init; }
    public string Token { get; init; } = null!;
    public DateTime TokenExpireDate { get; init; }
    public IEnumerable<string> Errors { get; init; } = null!;
}