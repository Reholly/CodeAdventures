using Shared.DTO;

namespace Shared.Responses.Index;

public record GetLastTidingResponse
{
    public required TidingModel Tiding { get; init; }
}