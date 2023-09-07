using Shared.DTO;

namespace Shared.Responses.Index;

public record UpdateTidingResponse
{
    public required TidingModel UpdatedTiding { get; init; }
}