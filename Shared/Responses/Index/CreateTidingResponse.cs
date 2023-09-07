using Shared.DTO;

namespace Shared.Responses.Index;

public record CreateTidingResponse
{
    public required TidingModel CreatedTiding { get; init; }
}