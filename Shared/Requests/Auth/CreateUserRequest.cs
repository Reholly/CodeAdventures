using MediatR;
using Shared.DTO;
using Shared.Responses.Auth;

namespace Shared.Requests.Auth;

public record CreateUserRequest : IRequest<CreateUserResponse>
{
    public required RegisterModel RegisterModel { get; init; }
}