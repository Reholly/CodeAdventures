using MediatR;
using Shared.Responses.Index;

namespace Shared.Requests.Index;

public record DeleteTidingRequest : IRequest<DeleteTidingResponse>
{
    public required DateTimeOffset PublicationDate { get; init; }
}