using Shared.DTO;

namespace Shared.Responses.Auth;

public class LogInResponse
{
   public required UserModel AuthenticatedUser { get; init; }
   public required TokenModel Token { get; init; }
   public required bool IsSucceeded { get; init; }
   public IEnumerable<string> Errors { get; init; } = null!;
}