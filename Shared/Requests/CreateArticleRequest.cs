using MediatR;
using Shared.Results;

namespace Shared.Requests;

public class CreateArticleRequest : IRequest<CreateArticleResult>  
{
    public int AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Text { get; set; } = null!;
}