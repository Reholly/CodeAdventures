using MediatR;
using Shared.Responses.Index;

namespace Shared.Requests.Index;

public record GetLastTidingRequest : IRequest<GetLastTidingResponse>;