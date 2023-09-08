using Microsoft.AspNetCore.Authorization;
using Server.Authorization.AuthorizationRequirements;
using Server.Services.ArticleServices;

namespace Server.Authorization.AuthorizationHandlers;

public class AuthorAndPersonnelOnlyHandler : AuthorizationHandler<AuthorAndPersonnelOnlyRequirement>
{
    private readonly IArticleService _articleService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthorAndPersonnelOnlyHandler(IArticleService articleService, IHttpContextAccessor httpContextAccessor)
    {
        _articleService = articleService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorAndPersonnelOnlyRequirement requirement)
    {
        var user = context.User;
        
        if (user.Identity is not { IsAuthenticated: true })
            context.Fail(new AuthorizationFailureReason(this, "Is not authenticated"));
        
        var articleId = int.Parse(_httpContextAccessor.HttpContext
            .GetRouteData().Values["id"]
            .ToString());

        var currentArticle = await _articleService.GetArticle(articleId)
                             ?? throw new NullReferenceException();
        
        if (currentArticle.Author.Email == user.FindFirst(c => c.Value == currentArticle.Author.Email)?.Value)
            context.Succeed(requirement);
        else if (user.HasClaim(c => c.Value is "moderatorRole" or "adminRole"))
            context.Succeed(requirement);
        else
            context.Fail(new AuthorizationFailureReason(this, "No permissions"));
    }
}