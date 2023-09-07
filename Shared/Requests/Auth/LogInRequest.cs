using MediatR;
using Shared.DTO;
using Shared.Responses.Auth;

namespace Shared.Requests.Auth;

public record LogInRequest : IRequest<LogInResponse>
{
    public required LoginModel LoginModel { get; init; }
}