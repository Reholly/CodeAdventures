namespace Shared.DTO;

public class TidingModel
{
    public string Title { get; init; } = null!;
    public string Text { get; init; } = null!;
    public DateTime PublicationDate { get; set; }
}