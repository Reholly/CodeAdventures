namespace Shared.DTO;

public class ArticleModel
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Description { get; set; } = null!;
}