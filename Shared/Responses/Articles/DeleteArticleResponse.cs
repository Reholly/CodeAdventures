namespace Shared.Responses.Articles;

public record DeleteArticleResponse
{
    public required bool IsSucceeded { get; init; }
    public ICollection<string> Errors { get; init; } = null!;
}