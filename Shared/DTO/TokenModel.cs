namespace Shared.DTO;

public record TokenModel
{
    public required string Token { get; init; } 
    public required DateTime ExpireTime { get; set; }
}