using System.Security.Claims;
using Shared.DTO;

namespace Shared.Responses.Auth;

public record CreateUserResponse
{
    public required UserModel? UserModel { get; init; }
}