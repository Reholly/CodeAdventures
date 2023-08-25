using Data.Entities;
using Data.Repositories;

namespace Server.Extensions;

public static class DependencyInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<Article>), typeof(ArticleRepository));
        services.AddScoped(typeof(IRepository<Contest>), typeof(ContestRepository));
        services.AddScoped(typeof(IRepository<Question>), typeof(QuestionRepository));
        services.AddScoped(typeof(IRepository<Test>), typeof(TestRepository));
        services.AddScoped(typeof(IRepository<Tiding>), typeof(TidingRepository));
        services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
    }
}