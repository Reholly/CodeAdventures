namespace Data.Entities;

public class Article
{
    public int Id { get; set; }
    public User Author { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTimeOffset PublicationDate { get; set; }
    public DateTimeOffset? EditDate { get; set; }
    public int Views { get; set; }
}