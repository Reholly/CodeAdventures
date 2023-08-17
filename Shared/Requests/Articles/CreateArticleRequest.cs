using MediatR;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public class CreateArticleRequest : IRequest<CreateArticleResponse>  
{
    public int AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Text { get; set; } = null!;
}