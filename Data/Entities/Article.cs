namespace Data.Entities;

public class Article
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime PublicationDate { get; set; }
    public DateTime? EditDate { get; set; }
}