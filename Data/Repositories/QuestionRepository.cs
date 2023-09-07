using Data.Entities;

namespace Data.Repositories;

public sealed class QuestionRepository : RepositoryBase<Question>
{
    public QuestionRepository(ApplicationDbContext context) : base(context) { }

    protected override List<Question> Entities => Context.Questions
        .ToList();

    public override async ValueTask<Question?> GetAsync(int id)
        => Entities.Find(question => question.Id == id);
}