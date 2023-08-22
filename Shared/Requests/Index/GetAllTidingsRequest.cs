using MediatR;
using Shared.Responses.Index;

namespace Shared.Requests.Index;

public record GetAllTidingsRequest : IRequest<GetAllTidingsResponse>;