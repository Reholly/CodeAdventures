using MediatR;
using Shared.Responses.Index;

namespace Shared.Requests.Index;

public record UpdateTidingRequest : IRequest<UpdateTidingResponse>
{
    public required string Title { get; init; }
    public required string Text { get; init; }
    public required DateTimeOffset PublicationDate { get; init; }
}