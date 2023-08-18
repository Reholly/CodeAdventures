namespace Data.Entities;

public class Tiding
{
    public int Id { get; init; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime PublicationDate { get; init; }
}