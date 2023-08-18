using MediatR;
using Shared.DTO;
using Shared.Responses.Index;

namespace Shared.Requests.Index;

public record CreateTidingRequest : IRequest<CreateTidingResponse>
{
    public required string Title { get; init; }
    public required string Text { get; init; }
}