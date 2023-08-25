using Shared.DTO;

namespace Shared.Responses.Index;

public record GetAllTidingsResponse
{
    public required ICollection<TidingModel> Tidings { get; init; }
}