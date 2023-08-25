using Shared.DTO;

namespace Shared.Responses.Auth;

public record LogInResponse
{
   public required UserModel AuthenticatedUser { get; init; }
   public required TokenModel Token { get; init; }
}