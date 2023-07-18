namespace Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public ICollection<Article> WrittenArticles = new List<Article>();
    public ICollection<Test> WrittenTests = new List<Test>();
    public ICollection<Contest> WrittenContests = new List<Contest>();
}