namespace Shared.DTO;

public class TidingModel
{
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset PublicationDate { get; set; }
}