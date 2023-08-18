using Shared.DTO;

namespace Shared.Responses.Index;

public class GetAllTidingsResponse
{
    public required ICollection<TidingModel> Tidings { get; init; }
}