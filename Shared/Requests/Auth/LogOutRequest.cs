using MediatR;
using Shared.Responses.Auth;

namespace Shared.Requests.Auth;

public record LogOutRequest : IRequest<LogOutResponse>;