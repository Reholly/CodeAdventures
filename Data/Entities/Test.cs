namespace Data.Entities;

public class Test
{
    public int Id { get; set; }
    public User Author { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Question> Questions { get; set; } = null!;
    public DateTimeOffset PublicationDate { get; set; }
    public bool IsHide { get; set; } = false;
}