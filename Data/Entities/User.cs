namespace Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<Article> WrittenArticles { get; set; } = new List<Article>();
    public ICollection<Test> WrittenTests { get; set; } = new List<Test>();
    public ICollection<Contest> WrittenContests { get; set; } = new List<Contest>();
}