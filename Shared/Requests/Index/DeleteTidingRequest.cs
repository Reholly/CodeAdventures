using MediatR;
using Shared.Responses.Index;

namespace Shared.Requests.Index;

public class DeleteTidingRequest : IRequest<DeleteTidingResponse>
{
    public required DateTime PublicationDate { get; init; }
}