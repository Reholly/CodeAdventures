using MediatR;
using Shared.Responses.Index;

namespace Shared.Requests.Index;

public class DeleteTidingRequest : IRequest<DeleteTidingResponse>
{
    public required DateTimeOffset PublicationDate { get; init; }
}